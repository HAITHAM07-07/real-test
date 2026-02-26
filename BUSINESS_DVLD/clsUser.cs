using BuisnessDVLD;
using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiusnessDVLD
{
    public class clsUser
    {


        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int UserID { get; set; }

        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }





        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            Username = " ";
            Password = " ";
            IsActive = false;


            EMode = Emode.addmode;



        }




        public clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.Username = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            EMode = Emode.updatamode;


        }




        private bool _AddUser()
        {



            this.UserID = clsDataUsers.AddOneUser(this.PersonID, this.Username, this.Password, this.IsActive);
            if (this.UserID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updateUser()
        {



            return clsDataUsers.UpdateOneUser(this.UserID, this.Username, this.Password, this.IsActive);


        }


        static public bool DeleteUser(int id)
        {

            return clsDataUsers.DeleteOneUser(id);

        }

        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddUser())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updateUser())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


        static public clsUser Find(int userid)
        {

            int PersonID = -1;
            string UserName = "", Password = "";
         
            bool isActive = false;



            if (clsDataUsers.GetUserByID(userid, ref PersonID, ref UserName, ref Password, ref isActive) != false)
            {
                return new clsUser(userid, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }


        }

        static public clsUser Find(string UserName,string Password)
        {

            int PersonID = -1, Userid = -1;
           

            bool isActive = false;



            if (clsDataUsers.GetUserByUsernameAndPassword(ref Userid, ref PersonID,  UserName,  Password, ref isActive) != false)
            {
                return new clsUser(Userid, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }


        }

        static public DataTable GetallDataUsers(string subquery, string letters)
        {
            return clsDataUsers.GetAllDataFromUsers(subquery, letters);
        }




        static public bool isExists(int id)
        {

            return clsDataUsers.IsExists(id);

        }


    }







}

