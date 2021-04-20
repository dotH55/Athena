/// <summary>
        /// This method is used for debugging purposes
        /// It generates SvcCalls and adds them to SvcCalls
        /// Also generate parts
        /// </summary>
        /// <returns>void</returns>
        public void GenerateSvc()
        {
            SvcCall temp1 = new SvcCall
            {
                Id = Guid.NewGuid().ToString(),
                SvcName = "EAST CENTRAL REGIONAL HOSPITAL",
                SvcDistance = 0,
                Status = "RS",
                ControlID = "G1360",
                Model = "MP301SPF",
                Problem = "MESSAGE: SENDER NAME NOT REGISTERED IN ADDRESS BOOK. -- NEED REMOTE SETUP --",
                DateTimeIn = "2018-09-17 10:58:05.087",
                ServicePhone = "770-123-4567",
                SvcFullAddress = "133 Herman St, Athens GA 30601",
                Notes = "Call 770-895-4796 before coming",
                Tech_Initials = "NULL",
                TakenBy = "TB",
                SvcDate = "03/17/2020",
                SvcTime = "11:38:16 AM",
                SvcSolution = ""
            };
            SvcCALLS.Add(temp1);

            SvcCall temp2 = new SvcCall
            {
                Id = Guid.NewGuid().ToString(),
                SvcName = "CANON SOLUTIONS AMERICA",
                SvcDistance = 0,
                Status = "INP",
                Tech_Initials = "DR",
                ControlID = "G1961",
                Model = "IR4525I",
                Problem = "HAD TO WIPE HIS COMPUTER AND NOW HE NEEDS HELP SETTING SCAN AND EMAIL BACK UP.  -- NEED REMOTE SETUP --",
                DateTimeIn = "2018-09-13 13:32:01.837",
                ServicePhone = "770-123-4567",
                SvcFullAddress = "1688 Windale Ct, Lawrenceville GA 30044",
                Notes = "Needs a new paper tray",
                TakenBy = "TB",
                SvcDate = "03/18/2020",
                SvcTime = "9:38:16 AM",
                SvcSolution = ""
            };
            SvcCALLS.Add(temp2);

            SvcCall temp3 = new SvcCall
            {
                Id = Guid.NewGuid().ToString(),
                SvcName = "NEW BEGINNINGS MED SERVICES/HAROLD WRIGHT",
                SvcDistance = 0,
                Status = "RS",
                Tech_Initials = "NULL",
                ControlID = "5817",
                Model = "C230SR",
                Problem = "MOVE MACHINE TO WRIGHT ONE ON GORDON HIGHWAY GO IN DELI KNOCK ON SIDE DOOR AND ASK FOR MS BLACK",
                DateTimeIn = "2018-08-31 10:45:38.047",
                ServicePhone = "770-123-4567",
                SvcFullAddress = "133 Herman Street, Athens GA 30601",
                Notes = "$125 PER DANIEL",
                TakenBy = "TB",
                SvcDate = "03/19/2020",
                SvcTime = "11:00:00 AM",
                SvcSolution = ""
            };
            SvcCALLS.Add(temp3);

            SvcCall temp4 = new SvcCall
            {
                Id = Guid.NewGuid().ToString(),
                SvcName = "NEW BEGINNINGS MED SERVICES/HAROLD WRIGHT",
                SvcDistance = 0,
                Status = "NEW",
                Tech_Initials = "NULL",
                ControlID = "5817",
                Model = "C230SR",
                Problem = "MOVE MACHINE TO WRIGHT ONE ON GORDON HIGHWAY GO IN DELI KNOCK ON SIDE DOOR AND ASK FOR MS BLACK",
                DateTimeIn = "2018-08-31 10:45:38.047",
                ServicePhone = "770-123-4567",
                SvcFullAddress = "133 Herman Street, Athens GA 30601",
                Notes = "$125 PER DANIEL",
                TakenBy = "TB",
                SvcDate = "04/20/2020",
                SvcTime = "04:20:00 PM",
                SvcSolution = ""
            };
            SvcCALLS.Add(temp4);

            SvcCall temp5 = new SvcCall
            {
                Id = Guid.NewGuid().ToString(),
                SvcName = "NEW BEGINNINGS MED SERVICES/HAROLD WRIGHT",
                SvcDistance = 0,
                Status = "NEW",
                Tech_Initials = "NULL",
                ControlID = "5817",
                Model = "C230SR",
                Problem = "MOVE MACHINE TO WRIGHT ONE ON GORDON HIGHWAY GO IN DELI KNOCK ON SIDE DOOR AND ASK FOR MS BLACK",
                DateTimeIn = "2018-08-31 10:45:38.047",
                ServicePhone = "770-123-4567",
                SvcFullAddress = "133 Herman Street, Athens GA 30601",
                Notes = "$125 PER DANIEL",
                TakenBy = "TB",
                SvcDate = "03/13/2020",
                SvcTime = "10:38:16 AM",
                SvcSolution = ""
            };
            SvcCALLS.Add(temp5);

            SvcCall temp6 = new SvcCall
            {
                Id = Guid.NewGuid().ToString(),
                SvcName = "NEW BEGINNINGS MED SERVICES/HAROLD WRIGHT",
                SvcDistance = 0,
                Status = "INP",
                Tech_Initials = "NULL",
                ControlID = "5817",
                Model = "C230SR",
                Problem = "MOVE MACHINE TO WRIGHT ONE ON GORDON HIGHWAY GO IN DELI KNOCK ON SIDE DOOR AND ASK FOR MS BLACK",
                DateTimeIn = "2018-08-31 10:45:38.047",
                ServicePhone = "770-123-4567",
                SvcFullAddress = "133 Herman Street, Athens GA 30601",
                Notes = "$125 PER DANIEL",
                TakenBy = "TB",
                SvcDate = "02/17/2020",
                SvcTime = "11:59:16 AM",
                SvcSolution = ""
            };
            SvcCALLS.Add(temp6);

            Part p1 = new Part("Part X", "7");
            Part p2 = new Part("Part 2", "9");
            Part p3 = new Part("Part 3", "10");
            Part p4 = new Part("Part 3", "10");
            Part p5 = new Part("Part 3", "10");
            SvcSOLUTION_PARTS.Add(p1);
            SvcSOLUTION_PARTS.Add(p2);
            SvcSOLUTION_PARTS.Add(p3);
            SvcSOLUTION_PARTS.Add(p4);
            SvcSOLUTION_PARTS.Add(p5);

            //SvcCalls.Sort(orderBy() );
            //SvcCalls.OrderBy(s => s.Status).ThenBy(o => o.SvcDistance);
            BubbleSort_Svc_By_Distance();
            BubbleSort_Svc_By_Status();
            //Sort_Svc_Status();
        } // GenerateSvc


/// <summary>
        /// This method claims a SvcCall
        /// Makes SQL call to update: @pSvcCallID, @pGivenTo, @pTimeGiven, @pLocation
        /// Make sure Svc_Selected has been updated before make any sql functions
        /// After making a SQL function, make sure to update the whole system 
        /// </summary>
        public static async void Claim_Svc_SQL_(bool claiming)
        {
            if (IsCONNECTED)
            {
                try
                {
                    // Start Connection
                    using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                    {
                        // Create a command object identifying the stored procedure
                        using (SqlCommand cmd = new SqlCommand(SQLServer.GetCLAIM_SVC_COMMAND(), conn))
                        {
                            // Set Parameters
                            // Set the command object so it knows to execute a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pSvcCallID", SqlDbType.VarChar).Value = SvcSELECTED.SvcCallID;
                            cmd.Parameters.Add("@pGivenTo", SqlDbType.VarChar).Value = claiming ? User.LOGGED_USER.Initials : "";
                            cmd.Parameters.Add("@pTimeGiven", SqlDbType.VarChar).Value = DateTime.Now.ToString();
                            cmd.Parameters.Add("@pLocation", SqlDbType.VarChar).Value = User.LOGGED_USER.Location;
                            cmd.Parameters.Add("@pStatus", SqlDbType.VarChar).Value = SvcSELECTED.Status;
                            cmd.CommandTimeout = 250;
                            conn.Open();

                            // TODO: make sure this command is right
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugWriteLine(ex.StackTrace);
                }
            }
            // Query SvcCalls
            _ = Query_Svc_SQL();
        }

/// <summary>
        /// This method is used to save a note in Database
        /// @pNotes is updated
        /// </summary>
        public static async Task Save_SvcNotes_SQL_()
        {
            if (IsCONNECTED)
            {
                try
                {
                    // Start Connection
                    using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                    {
                        // Create a command object identifying the stored procedure
                        using (SqlCommand cmd = new SqlCommand(SQLServer.GetSAVE_NOTES_COMMAND(), conn))
                        {
                            // Set Parameters
                            // Set the command object so it knows to execute a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pRowID", SqlDbType.VarChar).Value = SvcSELECTED.Rowguid;
                            cmd.Parameters.Add("@pNotes", SqlDbType.VarChar).Value = SvcSELECTED.Notes;
                            cmd.CommandTimeout = 250;
                            conn.Open();

                            // TODO: make sure this command is right
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugWriteLine("Claim_SvcSql" + ex.Message);
                }
            }
        }

/// <summary>
        /// This method complets a SvcCall
        /// Makes an SQL Call to update: @pSvcCallID, @pLocation, @pSolution, @pDateTimeStarted
        /// @pStatus, @pCallNumber, @pDateTimeCompleted, @pBlackMeter
        /// </summary>
        public static async Task Complet_Svc_SQL_(bool complet)
        {
            // Update Solution Parts

            //Changed 2020-05-05 KWS
            //Update_Solution_Parts_SQL();
            string partList = "";

            if (SvcSOLUTION_PARTS.Count != 0)
            {
                foreach (Part x in SvcSOLUTION_PARTS)
                {
                    partList += x.ID + "," + x.Quantity + "|";
                }
                // Delete the last comment
                partList = partList.Substring(0, partList.Length - 1);
            }

            // Check internet connection
            if (IsCONNECTED)
            {
                try
                {
                    // Start Connection
                    using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                    {
                        // Create a command object identifying the stored procedure
                        using (SqlCommand cmd = new SqlCommand(SQLServer.GetCOMPLET_SVC_COMMAND(), conn))
                        {
                            // Set Parameters
                            // Set the command object so it knows to execute a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pSvcCallID", SqlDbType.VarChar).Value = SvcSELECTED.SvcCallID;
                            cmd.Parameters.Add("@pLocation", SqlDbType.VarChar).Value = User.LOGGED_USER.Location;
                            cmd.Parameters.Add("@pSolution", SqlDbType.VarChar).Value = SvcSELECTED.SvcSolution; //SvcSELECTED.SvcSolution;
                            cmd.Parameters.Add("@pTimeStarted", SqlDbType.VarChar).Value = SvcSELECTED.SvcTimeBegin;
                            cmd.Parameters.Add("@pTimeCompleted", SqlDbType.VarChar).Value = SvcSELECTED.SvcTimeEnd;
                            cmd.Parameters.Add("@pBlackMeter", SqlDbType.Int).Value = int.Parse(SvcSELECTED.SvcBWMeter);
                            cmd.Parameters.Add("@pColorMeter", SqlDbType.Int).Value = int.Parse(SvcSELECTED.SvcColorMeter);
                            cmd.Parameters.Add("@pStatus", SqlDbType.VarChar).Value = SvcSELECTED.Status;
                            cmd.Parameters.Add("@pCallNumber", SqlDbType.Int).Value = int.Parse(SvcSELECTED.CallNumber);
                            cmd.Parameters.Add("@pTech_Init", SqlDbType.VarChar).Value = User.LOGGED_USER.Initials;
                            cmd.Parameters.Add("@pPart_Qty_List", SqlDbType.VarChar).Value = partList;
                            cmd.Parameters.Add("@pNew_Problem", SqlDbType.VarChar).Value = complet ? "" : SvcSELECTED.SvcNew_Problem;
                            cmd.CommandTimeout = 250;
                            conn.Open();

                            // TODO: make sure this command is right
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Do Nothing
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            // Query Database
            _ = Query_Svc_SQL();
        }

/// <summary>
        /// This method retrieves issued parts from Database
        /// Makes an SQL Call
        /// </summary>
        public static async Task<bool> Retrieve_Issued_Parts_SQL_()
        {
            // Check internet connection
            if (IsCONNECTED)
            {
                try
                {
                    // Start Connection
                    using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                    {
                        // Create a command object identifying the stored procedure
                        using (SqlCommand cmd = new SqlCommand(SQLServer.GetISSUED_PARTS_COMMAND(), conn))
                        {
                            // Set Parameters
                            // Set the command object so it knows to execute a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pSvcCallID", SqlDbType.VarChar).Value = SvcSELECTED.SvcCallID;
                            cmd.Parameters.Add("@pTech_Init", SqlDbType.VarChar).Value = User.LOGGED_USER.Initials;

                            cmd.CommandTimeout = 250;
                            conn.Open();

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                // Clear Solution parts
                                SvcSOLUTION_PARTS = new List<Part>();
                                // Iterate through results & adding them to entries
                                while (rdr.Read())
                                {
                                    Part temp = new Part(rdr);
                                    SvcSOLUTION_PARTS.Add(temp);
                                    /*
                                    Debug.WriteLine("*******************************************************************************************************************");
                                    Debug.WriteLine("*");
                                    Debug.WriteLine("*");
                                    Debug.WriteLine("*");
                                    Debug.WriteLine("** " + Convert.ToString(rdr["ItemNumber"]) + " " + Convert.ToString(rdr["Qty"]));
                                    Debug.WriteLine("*");
                                    Debug.WriteLine("*");
                                    Debug.WriteLine("*");
                                    Debug.WriteLine("*******************************************************************************************************************");*/
                                }
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                // TODO: 
                return false;
            }
        }


public static async Task<bool> Query_SQL_()
        {
            // Check internet connection
            if (IsCONNECTED)
            {
                try
                {
                    // Start Connection
                    using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                    {
                        // Create a command object identifying the stored procedure
                        using (SqlCommand cmd = new SqlCommand(SQLServer.GetACTIVE_SVC_COMMAND(), conn))
                        {
                            // Set Parameters
                            // Set the command object so it knows to execute a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pLocation", SqlDbType.VarChar).Value = User.LOGGED_USER.Location.ToString();
                            cmd.CommandTimeout = 250;
                            conn.Open();

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                // Clear Service Request List
                                SvcCALLS = new List<SvcCall>();
                                // Iterate through results & adding them to entries
                                while (rdr.Read())
                                {
                                    SvcCall temp = new SvcCall(rdr);
                                    //temp.Init_Distance(USER_CURR_LATITUDE, USER_CURR_LONGITUDE);
                                    SvcCALLS.Add(temp);
                                }
                                return true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                // TODO: 
                return false;
            }
        } // Query_SvcCalls_SQL

/// <summary>
        /// This method retrieves issued parts from Database
        /// Makes an SQL Call
        /// </summary>
        public static async Task Retrieve_Svc_History_SQL_()
        {
            SvcHISTORY_LIST = new ObservableCollection<JobClassModel>();
            // Check internet connection
            if (IsCONNECTED)
            {
                try
                {
                    // Start Connection
                    using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                    {
                        // Create a command object identifying the stored procedure
                        using (SqlCommand cmd = new SqlCommand(SQLServer.GetSVC_HISTORY_COMMAND(), conn))
                        {
                            // Set Parameters
                            // Set the command object so it knows to execute a stored procedure
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@pSvcCallID", SqlDbType.VarChar).Value = SvcSELECTED.SvcCallID;

                            cmd.CommandTimeout = 250;
                            conn.Open();

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                // Iterate through results & adding them to entries
                                while (rdr.Read())
                                {
                                    JobClassModel temp = new JobClassModel(rdr);
                                    Append_Svc_Parts(SvcHISTORY_LIST, temp);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO:
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }

/// <summary>
        /// This method claims a SvcCall
        /// Makes SQL call to update: @pSvcCallID, @pGivenTo, @pTimeGiven, @pLocation
        /// Make sure Svc_Selected has been updated before make any sql functions
        /// After making a SQL function, make sure to update the whole system 
        /// </summary>
        /// 

public SvcCall(SqlDataReader rdr)
        {
            Id = Guid.NewGuid().ToString();
            Rowguid = Convert.ToString(rdr["Rowguid"]);
            SerialNumber = Convert.ToString(rdr["SerialNumber"]);
            ControlID = Convert.ToString(rdr["ControlID"]);
            SvcCallID = Convert.ToString(rdr["SvcCallID"]);
            Model = Convert.ToString(rdr["Model"]);
            SvcName = Convert.ToString(rdr["SvcName"]);
            SvcAddr1 = Convert.ToString(rdr["SvcAddr1"]);
            SvcAddr2 = Convert.ToString(rdr["SvcAddr2"]);
            SvcCity = Convert.ToString(rdr["SvcCity"]);
            SvcState = Convert.ToString(rdr["SvcState"]);
            SvcZip = Convert.ToString(rdr["SvcZip"]);
            SvcCounty = Convert.ToString(rdr["SvcCounty"]);
            ServiceContact = Convert.ToString(rdr["ServiceContact"]);
            ServicePhone = Convert.ToString(rdr["ServicePhone"]);
            DateTimeIn = Convert.ToString(rdr["DateTimeIn"]);
            Problem = Convert.ToString(rdr["Problem"]);
            Notes = Convert.ToString(rdr["Notes"]);
            Status = Convert.ToString(rdr["Status"]);
            CallNumber = Convert.ToString(rdr["CallNumber"]);
            TakenBy = Convert.ToString(rdr["TakenBy"]);
            //changed 2020-05-05 KWS
            //temp.Tech_Initials = Convert.ToString(rdr["Tech_Initials"]);
            Tech_Initials = Convert.ToString(rdr["GivenTo"]);
            SvcDate = Convert.ToString(rdr["DateTimeIn"]).Substring(0, 9);
            //SvcDate = Convert.ToDateTime(rdr["DateTimeIn"]).ToString("DD/MM/YYYY");
            //changed 2020-05-05 KWS
            SvcTime = Convert.ToDateTime(rdr["DateTimeIn"]).ToString("HH:mm"); // Convert.ToString(rdr["DateTimeIn"]).Substring(12,5);
            SvcFullAddress = Convert.ToString(rdr["SvcAddr1"]) + " " + Convert.ToString(rdr["SvcAddr2"])
                + ", " + Convert.ToString(rdr["SvcCity"]) + " " + Convert.ToString(rdr["SvcState"]) + " " + Convert.ToString(rdr["SvcZip"]);

            SvcSolution = "";
        }

public JobClassModel(SqlDataReader rdr)
        {
            Invoice_Number = Convert.ToString(rdr["InvoiceNumber"]);
            Call_Number = Convert.ToString(rdr["CallNumber"]);
            Problem = Convert.ToString(rdr["Problem"]);
            Solution = Convert.ToString(rdr["Solution"]);
            Black_Meter = Convert.ToString(rdr["BlackMeter"]);
            Color_Meter = Convert.ToString(rdr["ColorMeter"]);
            DateTimeCompleted = Convert.ToString(rdr["DateTimeCompleted"]);
            Solution_Parts = Convert.ToString(rdr["QtyOrdered"]) + " x " + Convert.ToString(rdr["ItemNumber"])
                + " - " + Convert.ToString(rdr["Description"]) + " -{" + Convert.ToString(rdr["Tech"]) + "}";
        }


/// <summary>
        /// This method initiales Svc distance property 
        /// by calculation the user's distance from Svc address
        /// </summary>
        /// <param name="user_Curr_Latitude"> User Current Latitude</param>
        /// <param name="user_Curr_Longitude"> User Current Longitude</param>
        public async Task Init_Distance(double user_Curr_Latitude, double user_Curr_Longitude)
        {
            System.Collections.Generic.IEnumerable<Location> locations_To_Destination = await Geocoding.GetLocationsAsync(SvcFullAddress);
            Location location_To_Destination = locations_To_Destination.FirstOrDefault();

            if (location_To_Destination == null)
            {
                // Notify User
                SvcLatitude = 0;
                SvcLongitude = 0;
            }
            else
            {
                SvcLatitude = location_To_Destination.Latitude;
                SvcLongitude = location_To_Destination.Longitude;

                SvcDistance = (int)Xamarin.Essentials.Location.CalculateDistance(new Location(user_Curr_Latitude, user_Curr_Longitude),
                    new Location(SvcLatitude, SvcLongitude), DistanceUnits.Miles);
            }
        }

public Part(SqlDataReader rdr)
        {
            ID = Convert.ToString(rdr["ItemNumber"]);
            Quantity = Convert.ToString(rdr["Qty"]);
        }


        public User(SqlDataReader rdr)
        {
            //Copy from Ken

            //this.Rowguid = Convert.ToString(rdr["Rowguid"]);
            //this.First_Name = Convert.ToString(rdr["First_Name"]);
            //this.Last_Name = Convert.ToString(rdr["Last_Name"]);
            //this.Password = Convert.ToString(rdr["Password"]);
            try
            {
                Initials = Convert.ToString(rdr["Tech_Init"]);
                Username = Convert.ToString(rdr["Name"]);
                Location = Convert.ToString(rdr["Location"]);
                //Debug.WriteLine("* " + Convert.ToString(rdr["Name"]) + " : " + Convert.ToString(rdr["Password_Hash"]));
                Password = Convert.ToString(rdr["Password_Hash"]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


public static async void Query_Users_SQL_()
        {

            try
            {
                // Start Connection
                using (SqlConnection conn = new SqlConnection(SQLServer.GetSQL_CREDENTIALS()))
                {
                    // Create a command object identifying the stored procedure
                    using (SqlCommand cmd = new SqlCommand(SQLServer.GetUSERS_COMMAND(), conn))
                    {
                        // Set Parameters
                        // Set the command object so it knows to execute a stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 250;
                        conn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            // Iterate through results & adding them to entries
                            while (rdr.Read())
                            {

                                Debug.WriteLine("*******************************************************************************************************************");
                                Debug.WriteLine("*");
                                Debug.WriteLine("*");
                                Debug.WriteLine("*");
                                Debug.WriteLine("* " + Convert.ToString(rdr["Tech_Init"]) + " " + Convert.ToString(rdr["Name"]));
                                Debug.WriteLine("*");
                                Debug.WriteLine("*");
                                Debug.WriteLine("*******************************************************************************************************************");
                                USERS.Add(new User(rdr));
                            }
                        }
                    }
                }
            }
            catch
            {
                //DebugWriteLine(ex.Message);
                //throw new Exception("VPN");
                //_ = Application.Current.MainPage.DisplayAlert("Athena", "VPN", "OK");
                //Connectivity_Status = true;
            }
        }