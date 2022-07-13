using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Google.Cloud.Firestore;
using System.Collections;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime;
using System.Text.RegularExpressions;


namespace WindowsFormsApp1
{
    public partial class MainWindow : Form,Window
    {
        private CheckedListBox divisionsChecked;
        private Button newDivisionButton;
        private Button divisionDeleteButton;
        private Button infoDivisionButton;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private PictureBox picbox;
        private ZXing.BarcodeReader readerQR;
        private ZXing.BarcodeReader readerBar;

        private bool invokeInProgress = false;
        private bool stopInvoking = false;
        private bool permissionInfoScan = true;
        private Button newSessionButton;
        private Button departmentButton;
        private Label infoBox;
        private List<Division> allDiv = new List<Division>();
        private Label labelCity;
        private Thread clear;
        private City currentCity = new City();

        DateTime _lastKeystroke = new DateTime(0);
        private TextBox textBox1;
        private Button deleteAddressButton;
        private ComboBox videoDeviceComboBox;
        List<char> _barcode = new List<char>(20);

        public bool ShuttingDown { get { return stopInvoking; } }



        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            InfoLabel(await IsExistConnection());

            Program.dBworker.Attach(this);
            Program.dBworker.StartListenerFromDB_Divisions();
            
            FindVideoDevice();
            StartVideo();

            textBox1.Focus();
        }

        private async Task<City> IsExistConnection()
        {
            City curCity = new City();
            if (File.Exists(Path.Combine(Path.GetTempPath(), "depart.txt")))
            {
                StreamReader sr = new StreamReader(Path.Combine(Path.GetTempPath(), "depart.txt"));
                string mainColl = sr.ReadLine();
                string subColl = String.Empty;
                if (mainColl != null)
                {
                    subColl = sr.ReadLine();
                }

                sr.Close();

                if(mainColl != null)
                {
                    Program.dBworker.setCurCityId(mainColl);
                    Program.dBworker.setCurAddressID(subColl);
                    curCity = await Program.dBworker.IsExistConnection(mainColl, subColl, this);
                    currentCity.setId(curCity.getId());
                    currentCity.setName(curCity.getName());
                    List<Address> loc_addresses = new List<Address>();
                    foreach (Address address in curCity.getAddresses())
                    {
                        loc_addresses.Add(address);
                    }
                    currentCity.setAddresses(loc_addresses);

                    return currentCity;
                }
            }

            StartAuthenticationWindow();
            if((Program.dBworker.getCurCityId() != "")&&(Program.dBworker.getCurAddressID() != ""))
            {
                currentCity.setId(Program.dBworker.getCurCityId());
                currentCity.setName(Program.dBworker.getCurCity().getName());
                List<Address> loc_addresses1 = new List<Address>();
                loc_addresses1.AddRange(Program.dBworker.getCurCity().getAddresses());

                currentCity.setAddresses(loc_addresses1);
            }

            return currentCity;
        }
        public void StartAuthenticationWindow()
        {
            AuthenticationWindow aw = new AuthenticationWindow();
            aw.setCurrentCity(currentCity);
            aw.ShowDialog();
        }

        public void Update(List<Division> data)
        {
            allDiv.Clear();
            for(int i = 0; i < data.Count; i++)
            {
                allDiv.Add(data[i]);
            }
            UpdateView(allDiv);
        }

        private void UpdateView(List<Division> data)
        {
            Program.ToMain(divisionsChecked, (divisionsChecked) =>
            {
                ((CheckedListBox)divisionsChecked).Items.Clear();
                for (int i = 0; i < data.Count; i++)
                {
                    Division curr = data[i];
                    ((CheckedListBox)divisionsChecked).Items.Add(curr.getName());
                    SetCheckState(i, curr.getCheck());
                }
                textBox1.Focus();

                return true;
            });
        }

        private void AddNewDivision()
        {
            StartAddWindow();
            textBox1.Focus();
        }

        private void StartAddWindow()
        {
            AddWindow ad = new AddWindow();
            ad.ShowDialog();
        }
        private void DeleteDivision()
        {
            StartDeleteWindow();
            textBox1.Focus();
        }

        private void StartDeleteWindow()
        {
            DeleteWindow delWin = new DeleteWindow();
            delWin.ShowDialog();
        }

        private void newDivisionButton_Click(object sender, EventArgs e)
        {
            AddNewDivision();
        }

        private void divisionDeleteButton_Click(object sender, EventArgs e)
        {
            DeleteDivision();
        }

        private void codeDivisionButton_Click(object sender, EventArgs e)
        {
            CodeDivision();
        }

        private void CodeDivision()
        {
            StartCodeWindow();
            textBox1.Focus();
        }
        private void StartCodeWindow()
        {
            InfoWindow cd = new InfoWindow();
            cd.ShowDialog();
        }
        private void FindVideoDevice()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            readerQR = new ZXing.BarcodeReader();
            readerQR.Options.PossibleFormats = new List<ZXing.BarcodeFormat>();
            readerQR.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE);

            readerBar = new ZXing.BarcodeReader();
            readerBar.Options.PossibleFormats = new List<ZXing.BarcodeFormat>();
            readerBar.Options.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128);

            if (videoDevices.Count > 0)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    videoDeviceComboBox.Items.Add(device.Name);
                }

                videoDeviceComboBox.SelectedIndex = 0;
            }
        }

        private void StartVideo()
        {
            videoSource = new VideoCaptureDevice(videoDevices[videoDeviceComboBox.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if(this.Enabled)
            {
                if (picbox.InvokeRequired)
                {
                    if (stopInvoking != true) // don't start new invokes if the flag is set
                    {
                        invokeInProgress = true;  // let the form know if an invoke has started

                        Action safeUpdate = delegate
                        {
                            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                            picbox.Image = bitmap;
                        };
                        picbox.Invoke(safeUpdate);

                        invokeInProgress = false;  // the invoke is complete
                    }

                }
                else
                {
                    Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                    picbox.Image = bitmap;
                }


                ZXing.Result result1 = readerQR.Decode((Bitmap)eventArgs.Frame.Clone());
                ZXing.Result result2 = readerBar.Decode((Bitmap)eventArgs.Frame.Clone());

                if (result1 != null)
                {
                    CheckScannedItem(result1.Text);

                }

                if (result2 != null)
                {
                    CheckScannedItem(result2.Text);
                }
            }
        }

        private void CheckScannedItem(string id)
        {
            OutputInfo(id);
        }

        private void OutputInfo(string id)
        {
            for (int i = 0; i < allDiv.Count; i++)
            {
                if (allDiv[i].getId() == id)
                {
                    if (allDiv[i].getCheck())
                    {
                        if(permissionInfoScan)
                        {
                            string info = $"Отдел '{allDiv[i].getName()}' уже отсканирован";
                            int[] rgba = { 211, 211, 211, 255 };
                            SetInfoBox(info, rgba);
                            Block();
                        }
                    }
                    else
                    {
                        Program.dBworker.UpdateDivisionCheck(id, true);

                        string info = "Отсканировано: " + allDiv[i].getName();
                        int[] rgba = { 0, 255, 127, 255 };
                        SetInfoBox(info, rgba);
                        Block();
                    }
                    return;
                }
            }

            //if we did not leave earlier, then this id is not in the list of departments

            if(permissionInfoScan)
            {
                string newId = id;
                newId = Regex.Replace(newId, @"[^0-9a-zA-Z]+", "");
                if (id == newId)
                {
                    string infoError = "Код не принадлежит выбранному подразделению";
                    int[] rgbaError = { 220, 20, 60, 125 };
                    SetInfoBox(infoError, rgbaError);
                    Block();
                }
                else
                {
                    string infoException = "Переключите раскладку клавиатуры на английский язык для продолжения работы со сканером";
                    int[] rgbaException= { 255, 165, 0, 125 };
                    SetInfoBox(infoException, rgbaException);
                    Block();
                }
                
            }
        }

        private void Block()
        {
            if (clear != null)
            {
                if (clear.IsAlive)
                {
                    clear.Abort();
                }
            }
            permissionInfoScan = false;
            clear = new Thread(SetInfoBoxEmpty);
            clear.Start();
        }

        private void SetInfoBoxEmpty()
        {
            Thread.Sleep(2000);
            Program.ToMain(infoBox, (infoBox) =>
            {
                ((Label)infoBox).Text = String.Empty;
                Color color = Color.FromArgb(0, 0, 0, 0);
                infoBox.BackColor = color;
                return true;
            });
            permissionInfoScan=true;
        }

        private void SetInfoBox(string info, int[] rgba)
        {
            Program.ToMain(infoBox, (infoBox) =>
            {
                ((Label)infoBox).Text = info;
                Color color = Color.FromArgb(rgba[3], rgba[0], rgba[1], rgba[2]);
                infoBox.BackColor = color;
                return true;
            });
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosingVideo_MainWindow(e);
        }

        private async void ClosingVideo_MainWindow(FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
                if (invokeInProgress)
                {
                    e.Cancel = true;  // cancel the original event 

                    stopInvoking = true; // advise to stop taking new work

                    // now wait until current invoke finishes
                    await Task.Factory.StartNew(() =>
                    {
                        while (invokeInProgress) ;
                    });
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                    // now close the form
                    this.Close();
                }

            }
        }

        private void divisionsChecked_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartChangeDataWindow();
        }

        private void StartChangeDataWindow()
        {
            int index = divisionsChecked.SelectedIndex;
            if(index != -1)
            {
                ChangeDataWindow chdata = new ChangeDataWindow(allDiv[index]);
                chdata.ShowDialog();
            }
            textBox1.Focus();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.divisionsChecked = new System.Windows.Forms.CheckedListBox();
            this.newDivisionButton = new System.Windows.Forms.Button();
            this.divisionDeleteButton = new System.Windows.Forms.Button();
            this.infoDivisionButton = new System.Windows.Forms.Button();
            this.picbox = new System.Windows.Forms.PictureBox();
            this.newSessionButton = new System.Windows.Forms.Button();
            this.departmentButton = new System.Windows.Forms.Button();
            this.infoBox = new System.Windows.Forms.Label();
            this.labelCity = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.deleteAddressButton = new System.Windows.Forms.Button();
            this.videoDeviceComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // divisionsChecked
            // 
            this.divisionsChecked.FormattingEnabled = true;
            this.divisionsChecked.Location = new System.Drawing.Point(11, 32);
            this.divisionsChecked.Name = "divisionsChecked";
            this.divisionsChecked.Size = new System.Drawing.Size(537, 344);
            this.divisionsChecked.TabIndex = 0;
            this.divisionsChecked.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.divisionsChecked_ItemCheck);
            this.divisionsChecked.SelectedIndexChanged += new System.EventHandler(this.divisionsChecked_SelectedIndexChanged);
            // 
            // newDivisionButton
            // 
            this.newDivisionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.newDivisionButton.Location = new System.Drawing.Point(11, 391);
            this.newDivisionButton.Name = "newDivisionButton";
            this.newDivisionButton.Size = new System.Drawing.Size(175, 52);
            this.newDivisionButton.TabIndex = 2;
            this.newDivisionButton.Text = "Добавить подразделение";
            this.newDivisionButton.UseVisualStyleBackColor = true;
            this.newDivisionButton.Click += new System.EventHandler(this.newDivisionButton_Click);
            // 
            // divisionDeleteButton
            // 
            this.divisionDeleteButton.Location = new System.Drawing.Point(192, 391);
            this.divisionDeleteButton.Name = "divisionDeleteButton";
            this.divisionDeleteButton.Size = new System.Drawing.Size(175, 52);
            this.divisionDeleteButton.TabIndex = 3;
            this.divisionDeleteButton.Text = "Удалить подразделение";
            this.divisionDeleteButton.UseVisualStyleBackColor = true;
            this.divisionDeleteButton.Click += new System.EventHandler(this.divisionDeleteButton_Click);
            // 
            // infoDivisionButton
            // 
            this.infoDivisionButton.Location = new System.Drawing.Point(373, 462);
            this.infoDivisionButton.Name = "infoDivisionButton";
            this.infoDivisionButton.Size = new System.Drawing.Size(175, 52);
            this.infoDivisionButton.TabIndex = 4;
            this.infoDivisionButton.Text = "Информация по подразделениям";
            this.infoDivisionButton.UseVisualStyleBackColor = true;
            this.infoDivisionButton.Click += new System.EventHandler(this.codeDivisionButton_Click);
            // 
            // picbox
            // 
            this.picbox.Location = new System.Drawing.Point(554, 32);
            this.picbox.Name = "picbox";
            this.picbox.Size = new System.Drawing.Size(630, 344);
            this.picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbox.TabIndex = 8;
            this.picbox.TabStop = false;
            // 
            // newSessionButton
            // 
            this.newSessionButton.Location = new System.Drawing.Point(373, 391);
            this.newSessionButton.Name = "newSessionButton";
            this.newSessionButton.Size = new System.Drawing.Size(175, 52);
            this.newSessionButton.TabIndex = 10;
            this.newSessionButton.Text = "Начать новый сеанс";
            this.newSessionButton.UseVisualStyleBackColor = true;
            this.newSessionButton.Click += new System.EventHandler(this.newSessionButton_Click);
            // 
            // departmentButton
            // 
            this.departmentButton.Location = new System.Drawing.Point(11, 462);
            this.departmentButton.Name = "departmentButton";
            this.departmentButton.Size = new System.Drawing.Size(175, 52);
            this.departmentButton.TabIndex = 11;
            this.departmentButton.Text = "Поменять рабочий адрес";
            this.departmentButton.UseVisualStyleBackColor = true;
            this.departmentButton.Click += new System.EventHandler(this.departmentButton_Click);
            // 
            // infoBox
            // 
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoBox.Location = new System.Drawing.Point(554, 391);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(630, 52);
            this.infoBox.TabIndex = 13;
            this.infoBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(13, 10);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(0, 16);
            this.labelCity.TabIndex = 14;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(940, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 22);
            this.textBox1.TabIndex = 15;
            // 
            // deleteAddressButton
            // 
            this.deleteAddressButton.Location = new System.Drawing.Point(192, 462);
            this.deleteAddressButton.Name = "deleteAddressButton";
            this.deleteAddressButton.Size = new System.Drawing.Size(175, 52);
            this.deleteAddressButton.TabIndex = 16;
            this.deleteAddressButton.Text = "Удалить текущий адрес";
            this.deleteAddressButton.UseVisualStyleBackColor = true;
            this.deleteAddressButton.Click += new System.EventHandler(this.deleteAddressButton_Click);
            // 
            // videoDeviceComboBox
            // 
            this.videoDeviceComboBox.FormattingEnabled = true;
            this.videoDeviceComboBox.ItemHeight = 16;
            this.videoDeviceComboBox.Location = new System.Drawing.Point(554, 462);
            this.videoDeviceComboBox.Name = "videoDeviceComboBox";
            this.videoDeviceComboBox.Size = new System.Drawing.Size(630, 24);
            this.videoDeviceComboBox.TabIndex = 17;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1202, 533);
            this.Controls.Add(this.videoDeviceComboBox);
            this.Controls.Add(this.deleteAddressButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelCity);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.departmentButton);
            this.Controls.Add(this.newSessionButton);
            this.Controls.Add(this.picbox);
            this.Controls.Add(this.infoDivisionButton);
            this.Controls.Add(this.divisionDeleteButton);
            this.Controls.Add(this.newDivisionButton);
            this.Controls.Add(this.divisionsChecked);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Учет подразделений";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void divisionsChecked_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(divisionsChecked.Items[e.Index].ToString(), 
                                                        divisionsChecked.Font, divisionsChecked.Width).Height;
        }

        private void divisionsChecked_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (divisionsChecked.Items.Count > 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(divisionsChecked.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }
        public void SetCheckState(int itemIndex, bool check)
        {
            CheckState newState;
            if (check)
            {
                newState = CheckState.Checked;
            }
            else
            {
                newState = CheckState.Unchecked;
            }

            divisionsChecked.ItemCheck -= divisionsChecked_ItemCheck; // отключаем обработчик
            divisionsChecked.SetItemCheckState(itemIndex, newState); // меняем состояние
            divisionsChecked.ItemCheck += divisionsChecked_ItemCheck; // подключаем обработчик
        }
        private void divisionsChecked_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            e.NewValue = e.CurrentValue;
            textBox1.Focus();
        }

        private void newSessionButton_Click(object sender, EventArgs e)
        {
            StartNewSession();
        }

        private void StartNewSession()
        {
            for (int i = 0; i < allDiv.Count; i++)
            {
                Program.dBworker.UpdateDivisionCheck(allDiv[i].getId(), false);
            }
            textBox1.Focus();
        }

        private void departmentButton_Click(object sender, EventArgs e)
        {
            SelectDepartment();
        }

        private async void SelectDepartment()
        {
            this.Enabled = false;
            Program.dBworker.UnsubscribeListener_Divisions();

            StartAuthenticationWindow();

            bool result = await Program.dBworker.GetAddressesFromDB();
            Program.dBworker.StartListenerFromDB_Divisions();
            Program.dBworker.NotifyWin();
            this.Enabled = true;

            newDivisionButton.Enabled = true;
            divisionDeleteButton.Enabled = true;
            infoDivisionButton.Enabled = true;
            newSessionButton.Enabled = true;
            deleteAddressButton.Enabled = true;

            currentCity = Program.dBworker.getCurCity();
            InfoLabel(currentCity);
            textBox1.Focus();
        }

        private void InfoLabel(City city)
        {
            int addressIndex = Program.dBworker.getCurAddressIndex();
            if ((city != null)&&(city.getAddresses().Count != 0)&&(addressIndex != -1))
            {
                if ((city.getName() != null) && (city.getAddresses()[addressIndex].getName() != null))
                {
                    Program.ToMain(labelCity, (labelCity) =>
                    {
                        string curCity = city.getName();
                        string curAddress = city.getAddresses()[addressIndex].getName();
                        labelCity.Text = $"Город выбранного подразделения: {curCity}, адрес: {curAddress}";
                        return true;
                    });
                }
            }
            else
            {
                labelCity.Text = "";
            }
            
        }
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dBworker.Detach(this);
            if (Program.dBworker.ListenerFromDB_CitiesExist())
                Program.dBworker.UnsubscribeListener_Cities();
            if (Program.dBworker.ListenerFromDB_AddressesExist())
                Program.dBworker.UnsubscribeListener_Addresses();
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            if (elapsed.TotalMilliseconds > 100)
                _barcode.Clear();     
            
            if (e.KeyChar == 13 && _barcode.Count > 0)
            {
                string code = new String(_barcode.ToArray());
                CheckScannedItem(code);
                _barcode.Clear();
            }
            else
            {
                _barcode.Add((char) e.KeyChar);
                
                _lastKeystroke = DateTime.Now;
            }
            textBox1.Clear();
        }

        private void deleteAddressButton_Click(object sender, EventArgs e)
        {
            DeleteAddressButton();
        }

        private void DeleteAddressButton()
        {
            City city = Program.dBworker.getCurCity();
            Address address = Program.dBworker.getCurAddress();
            if ((city != null)&&(address != null))
            {
                string cityId = city.getId();
                string addressId = address.getId();

                DialogResult dialogResult = MessageBox.
                            Show("Вы действительно хотите удалить адрес  " + address.getName() + " ?",
                            "Удаление адреса из базы данных", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    Program.dBworker.DeleteAddress(cityId, addressId);

                    newDivisionButton.Enabled = false;
                    divisionDeleteButton.Enabled = false;
                    infoDivisionButton.Enabled=false;
                    newSessionButton.Enabled=false;
                    deleteAddressButton.Enabled=false;

                    Program.dBworker.setCurAddressID("-1");
                    Program.dBworker.setCurCityId("-1");
                    InfoLabel(Program.dBworker.getCurCity());
                    textBox1.Focus();
                }
                else if (dialogResult == DialogResult.No)
                {
                    textBox1.Focus();
                    return;
                }
            }
        }
    }
}
