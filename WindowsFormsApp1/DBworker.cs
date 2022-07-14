using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public class DBworker : Worker
    {
        private List<Window> windows = new List<Window>();
        private List<Authentication> authentications = new List<Authentication>();
        private FirestoreDb database;
        private List<Division> allDiv = new List<Division>();
        private int curCityIndex = -1;
        private int curAddressIndex = -1;
        private string curCityId = String.Empty;
        private string curAddressId = String.Empty;
        private List<City> cities = new List<City>();
        private List<Address> addresses = new List<Address>();
        private FirestoreChangeListener listenerDivisions;
        private FirestoreChangeListener listenerCities;
        private FirestoreChangeListener listenerAddresses;

        private string mainCollection = "cities";
        private string subCollection = "addresses";

        private string city_field1 = "name";

        private string address_field1 = "address";
        private string address_field2 = "divisions";

        private string division_field1 = "name";
        private string division_field2 = "checked";
        private string division_field3 = "fio";
        private string division_field4 = "floor";
        private string division_field5 = "number";
        private string division_field6 = "phone";
        private string division_field7 = "wing";


        public void Attach(Window window)
        {
            this.windows.Add(window);
            window.Update(allDiv);
            
        }

        public void Attach(Authentication authentication)
        {
            this.authentications.Add(authentication);
            authentication.UpdateCities(cities);
        }

        public void Detach(Window window)
        {
            this.windows.Remove(window);
        }

        public void Detach(Authentication authentication)
        {
            this.authentications.Remove(authentication);

        }
        
        public void NotifyWin()
        {
            foreach (Window window in this.windows)
            {
                window.Update(allDiv);
            }
        }

        public void NotifyAuth_Cities()
        {
            List<City> cityList = new List<City>();
            cityList.AddRange(cities);

            foreach (Authentication auth in this.authentications)
            {
                auth.UpdateCities(cityList);
            }
        }

        public void NotifyAuth_Addresses()
        {
            foreach (Authentication auth in this.authentications)
            {
                auth.UpdateAddresses(addresses);
            }
        }

        public DBworker()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"sberqrscanner-firebase-adminsdk-7a0b8-e59341e189.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            database = FirestoreDb.Create("sberqrscanner");

            Loaddata();
        }

        private async void Loaddata()
        {
            await GetCitiesFromDB();
        }
        public List<Division> GetData()
        {
            return allDiv;
        }

        public void StartListenerFromDB_Divisions()
        {
            ListenerFromDB_Divisions();
        }
        private void ListenerFromDB_Divisions()
        {
            if((curAddressIndex != -1)&&(curCityIndex != -1)&&(cities.Count !=0)&&(addresses.Count != 0))
            {
                curCityId = cities[curCityIndex].getId();
                curAddressId = addresses[curAddressIndex].getId();
            }
            
            
            if((curCityId != "")&&(curAddressId != "")&& (curCityId != null) && (curAddressId != null))
            {
                Query Qref = database.Collection(mainCollection)
                .Document(curCityId).Collection(subCollection)
                .Document(curAddressId).Collection(address_field2);


                listenerDivisions = Qref.Listen(snapshot =>
                {
                    allDiv.Clear();
                    foreach (DocumentSnapshot docsnap in snapshot)
                    {
                        if (docsnap.Exists)
                        {
                            Division dv = new Division(docsnap.Id,
                                docsnap.GetValue<String>(division_field1),
                                docsnap.GetValue<bool>(division_field2),
                                docsnap.GetValue<String>(division_field3),
                                docsnap.GetValue<int>(division_field4),
                                docsnap.GetValue<int>(division_field5),
                                docsnap.GetValue<String>(division_field6),
                                docsnap.GetValue<String>(division_field7));
                            allDiv.Add(dv);
                        }
                    }

                    var threadParameters = new System.Threading.ThreadStart(delegate { NotifyWin(); });
                    var thread2 = new System.Threading.Thread(threadParameters);
                    thread2.Start();
                });
            }
        }

        public void StartListenerFromDB_Cities()
        {
            ListenerFromDB_Cities();
        }
        private void ListenerFromDB_Cities()
        {
            Query Qref = database.Collection(mainCollection);

            listenerCities = Qref.Listen(snapshot =>
            {
                cities.Clear();
                foreach (DocumentSnapshot docsnap in snapshot)
                {
                    if (docsnap.Exists)
                    {
                        City ct = new City(docsnap.Id, docsnap.GetValue<String>(city_field1));
                        cities.Add(ct);
                    }
                }
                var threadParameters = new System.Threading.ThreadStart(delegate { NotifyAuth_Cities(); });
                var thread2 = new System.Threading.Thread(threadParameters);
                thread2.Start();
            });
        }

        public void StartListenerFromDB_Addresses(string id)
        {
            ListenerFromDB_Addresses(id);
            NotifyAuth_Addresses();
        }
        private void ListenerFromDB_Addresses(string idCity)
        {
            Query Qref = database.Collection(mainCollection)
                .Document(idCity).Collection(subCollection);

            listenerAddresses = Qref.Listen(snapshot =>
            {
                addresses.Clear();
                foreach (DocumentSnapshot docsnap in snapshot)
                {
                    if (docsnap.Exists)
                    {
                        Address ad = new Address(docsnap.Id, docsnap.GetValue<String>(address_field1));
                        addresses.Add(ad);
                    }
                }
                var threadParameters = new System.Threading.ThreadStart(delegate { NotifyAuth_Addresses(); });
                var thread2 = new System.Threading.Thread(threadParameters);
                thread2.Start();
            });
        }

        public async void UnsubscribeListener_Divisions()
        {
            if(listenerDivisions != null)
            {
                await listenerDivisions.StopAsync();
            }
        }

        public void UnsubscribeListener_Cities()
        {
            if (listenerCities != null)
            {
                listenerCities.StopAsync();
            }
        }

        public void UnsubscribeListener_Addresses()
        {
            if (listenerAddresses != null)
            {
                listenerAddresses.StopAsync();
            }
        }

        public bool ListenerFromDB_CitiesExist()
        {
            if (listenerCities != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ListenerFromDB_AddressesExist()
        {
            if(listenerAddresses != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddNewDivision(string name, string fio, int floor, int number, string phone, string wing)
        {
            CollectionReference col = database.Collection(mainCollection)
                .Document(curCityId).Collection(subCollection)
                .Document(curAddressId).Collection(address_field2);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {division_field1, name },
                {division_field2, false},
                {division_field3, fio },
                {division_field4, floor },
                {division_field5, number },
                {division_field6, phone },
                {division_field7, wing }
            };
            col.AddAsync(data);
        }

        public void DeleteDivision(string divId)
        {
            DocumentReference doc = database.Collection(mainCollection)
                .Document(curCityId).Collection(subCollection)
                .Document(curAddressId).Collection(address_field2)
                .Document(divId);
            doc.DeleteAsync();
        }

        public async void UpdateDivisionCheck(string id, bool check)
        {
            bool flag = false;

            for(int i = 0; i < allDiv.Count; i++)
            {
                if(id == allDiv[i].getId())
                {
                    flag = true;
                }
            }
            if(flag)
            {
                DocumentReference doc = database.Collection(mainCollection)
                .Document(curCityId).Collection(subCollection)
                .Document(curAddressId).Collection(address_field2)
                .Document(id);

                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"checked", check }
                };

                DocumentSnapshot snap = await doc.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await doc.UpdateAsync(data);
                }
            }
        }

        public async void AddNewAddress(string cityName, string addressName)
        {
            City city = new City();
            string idCity = null;
            string idAddress = null;

            for(int i = 0; i < cities.Count; i++)
            {
                if(cities[i].getName() != null)
                {
                    if (cityName == cities[i].getName())
                    {
                        idCity = cities[i].getId();
                        city = cities[i];
                        bool result = await Program.dBworker.GetAddressesFromDB(i);
                    }
                }
                
            }

            if((idCity != "")&&(idCity != null))
            {
                if (city.getAddresses() == null)
                {
                    CollectionReference col = database.Collection(mainCollection)
                        .Document(idCity).Collection(subCollection);

                    Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {address_field1, addressName }
                    };
                    col.AddAsync(data);
                }
                else
                {
                    for (int i = 0; i < city.getAddresses().Count; i++)
                    {
                        if (city.getAddresses()[i].getName() != null)
                        {
                            if (city.getAddresses()[i].getName() == addressName)
                            {
                                idAddress = city.getAddresses()[i].getId();
                            }
                        }
                    }

                    if (idAddress == null)
                    {
                        CollectionReference col = database.Collection(mainCollection)
                        .Document(idCity).Collection(subCollection);

                        Dictionary<string, object> data = new Dictionary<string, object>()
                        {
                            {address_field1, addressName }
                        };
                        col.AddAsync(data);
                    }
                }
            }
            else
            {
                CollectionReference col = database.Collection(mainCollection);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {city_field1, cityName }
                };
                DocumentReference doc = await col.AddAsync(data);

                CollectionReference col1 = doc.Collection(subCollection);

                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                    {address_field1, addressName }
                };
                col1.AddAsync(data1);
            } 
        }

        public async void DeleteAddress(string cityId, string addressId)
        {
            if(cityId != null)
            {
                if(addressId != null)
                {
                    DocumentReference doc = database.Collection(mainCollection).Document(cityId)
                                                        .Collection(subCollection).Document(addressId);
                    doc.DeleteAsync();

                    CollectionReference Query = database.Collection(mainCollection).Document(cityId)
                                                        .Collection(subCollection);
                    QuerySnapshot snap = await Query.GetSnapshotAsync();

                    if(snap.Count == 0)
                    {
                        DocumentReference docCity = database.Collection(mainCollection).Document(cityId);
                        docCity.DeleteAsync();
                    }
                }
            }
        }
        
        public async Task<bool> GetCitiesFromDB()
        {
            QuerySnapshot snap = null;
            try
            {
                Query Qref = database.Collection(mainCollection);
                snap =  await Qref.GetSnapshotAsync();
            }
            catch
            {
                //attempts to handle startup when there is no internet
                MessageBox.Show("Проверте подключение к интернету и перезапустите приложение");
                Application.Exit();
                //Thread.Sleep(5000);
            }

            cities.Clear();

            foreach (DocumentSnapshot docsnap in snap)
            {
                if (docsnap.Exists)
                {
                    City city = new City(docsnap.Id, docsnap.GetValue<String>(city_field1));
                    cities.Add(city);
                }
            }

            if ((curCityId != "") && (curAddressId != ""))
            {
                for(int i = 0;  i < cities.Count;i++)
                {
                    if(cities[i].getId() == curCityId)
                    {
                        curCityIndex = i;
                        await GetAddressesFromDB();

                        for (int j = 0; j < cities[i].getAddresses().Count; j++)
                        {
                            if(cities[i].getAddresses()[j] != null)
                            {
                                if (cities[i].getAddresses()[j].getId() == curAddressId)
                                {
                                    curAddressIndex = j;
                                }
                            }
                        }
                    }
                }
            }

            NotifyAuth_Cities();
            return true;
        }

        public async Task<bool> GetAddressesFromDB()
        {
            if(curCityIndex != -1)
            {
                Query doc = database.Collection(mainCollection)
                .Document(cities[curCityIndex].getId()).Collection(subCollection);
                QuerySnapshot snap = await doc.GetSnapshotAsync();

                addresses.Clear();
                foreach (DocumentSnapshot docsnap in snap)
                {
                    if (docsnap.Exists)
                    {
                        Address address = new Address(docsnap.Id, docsnap.GetValue<String>(address_field1));
                        addresses.Add(address);
                    }
                }
                cities[curCityIndex].setAddresses(addresses);
                NotifyAuth_Addresses();
            }

            return true;
        }

        public async Task<bool> GetAddressesFromDB(int indexCity)
        {
            if(indexCity != -1)
            {
                Query doc = database.Collection(mainCollection)
                .Document(cities[indexCity].getId()).Collection(subCollection);
                QuerySnapshot snap = await doc.GetSnapshotAsync();

                addresses.Clear();
                foreach (DocumentSnapshot docsnap in snap)
                {
                    if (docsnap.Exists)
                    {
                        Address address = new Address(docsnap.Id, docsnap.GetValue<String>(address_field1));
                        addresses.Add(address);
                    }
                }
                cities[indexCity].setAddresses(addresses);
                NotifyAuth_Addresses();
            }

            return true;
        }

        public void setCurCityIndex(int index)
        {
            curCityIndex = index;
            GetAddressesFromDB();
        }

        public void setCurCityId(string id)
        {
            curCityId = id;
            if(curCityId != "-1")
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    if (cities[i].getId() == curCityId)
                    {
                        curCityIndex = i;
                    }
                }
                GetAddressesFromDB();
            }
            else
            {
                curCityIndex = -1;
            }

        }

        public void setCurAddressIndex(int index)
        {
            curAddressIndex = index;
        }

        public void setCurAddressID(string id)
        {
            curAddressId = id;
            if (curAddressId != "-1")
            {
                for (int i = 0; i < addresses.Count; i++)
                {
                    if (cities[i].getId() == curAddressId)
                    {
                        curAddressIndex = i;
                    }
                }
            }
            else
            {
                curAddressIndex = -1;
            }
        }

        public int getCurCityIndex()
        {
            return curCityIndex;
        }

        public string getCurCityId()
        {
            if(curCityIndex != -1)
            {
                curCityId = cities[curCityIndex].getId();
            }
            return curCityId;
        }

        public int getCurAddressIndex()
        {
            return curAddressIndex;
        }

        public string getCurAddressID()
        {
            if (curAddressIndex != -1)
            {
                curAddressId = addresses[curAddressIndex].getId();
            }
            return curAddressId;
        }

        public City getCurCity()
        {
            if((cities.Count == 0)||(curCityIndex == -1))
            {
                return null;
            }
            else
            {
                GetAddressesFromDB();
                return cities[curCityIndex];
            }
        }

        public Address getCurAddress()
        {
            if((addresses.Count == 0)||(curAddressIndex == -1))
            {
                Address address = new Address("-1", "-1");
                return address;
            }
            else
            {
                return addresses[curAddressIndex];
            }
        }

        public async Task<City> IsExistConnection(string mainId, string subId, MainWindow mw)
        {
            City city = new City();
            List<Address> loc_addresses = new List<Address>();

            if ((mainId != "") && (subId != "")&& (mainId != null))
            {
                DocumentReference doc1 = database.Collection(mainCollection)
                                .Document(mainId);
                DocumentSnapshot snap1 = await doc1.GetSnapshotAsync();
                if(snap1.Exists)
                {
                    city.setId(mainId);
                    city.setName(snap1.GetValue<String>(city_field1));

                    DocumentReference doc2 = database.Collection(mainCollection)
                                .Document(mainId).Collection(subCollection).Document(subId);
                    DocumentSnapshot snap2 = await doc2.GetSnapshotAsync();

                    if(snap2.Exists)
                    {
                        CollectionReference col = database.Collection(mainCollection)
                                    .Document(mainId).Collection(subCollection);
                        QuerySnapshot snapCol = await col.GetSnapshotAsync();


                        for (int i = 0; i < snapCol.Count; i++)
                        {
                            if (snapCol[i].Exists)
                            {
                                Address address = new Address(snapCol[i].Id, snapCol[i].GetValue<String>(address_field1));
                                loc_addresses.Add(address);

                                if (snapCol[i].Id == subId)
                                {
                                    curAddressIndex = i;
                                }
                            }
                        }

                        if (curAddressIndex == -1)
                        {
                            mw.StartAuthenticationWindow();
                            loc_addresses.Clear();
                            for (int i = 0; i < addresses.Count; i++)
                            {
                                loc_addresses.Add(addresses[i]);

                                if (addresses[i].getId() == subId)
                                {
                                    curAddressIndex = i;
                                }
                            }
                        }
                        city.setAddresses(loc_addresses);

                        return city;
                    }
                }
            }

            mw.StartAuthenticationWindow();
            if(curCityIndex > 0)
            {
                city.setId(cities[curCityIndex].getId());
                city.setName(cities[curCityIndex].getName());
                loc_addresses.Clear();
                loc_addresses.AddRange(addresses);
                city.setAddresses(loc_addresses);

            }

            return city;
        }
    }
}
