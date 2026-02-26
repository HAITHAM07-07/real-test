using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace BuisnessDVLD
{


    public class clsBuisnessPeople
    {
        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }




        public clsBuisnessPeople()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = " ";
            SecondName = " ";
            ThirdName = " ";
            LastName = " ";

            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Address = " ";
            Phone = " ";


            Email = " ";
            NationalityCountryID = -1;
            ImagePath = " ";

            EMode = Emode.addmode;



        }




        public clsBuisnessPeople(int PersonID, string NationalNo, string First, string second, string third, string last, int gendor,
              string address, string phone, string email, int NationalityCountryID, string Image, DateTime Dateofbirth)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = First;
            this.SecondName = second;
            this.ThirdName = third;
            this.LastName = last;
            this.DateOfBirth = Dateofbirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = Image;

            EMode = Emode.updatamode;


        }




        private bool _AddPerson()
        {



            this.PersonID = clsDatabasePeople.AddOnePerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Gendor,
                this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath, this.DateOfBirth);
            if (this.PersonID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updatePerson()
        {



            return clsDatabasePeople.UpdateOnePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Gendor,
                   this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath, this.DateOfBirth);


        }


        static public bool DeletePerson(int id)
        {

            return clsDatabasePeople.DeleteOnePerson(id);

        }

        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddPerson())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updatePerson())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


        static public clsBuisnessPeople Find(int id)
        {
            string nationalno = "";
            int nationalCountryID = 1, gendor = 0;
            string firstName = "", secondName = "", thirdName = "", lastname = "", address = "", Email = "", Phone = "", imagepath = "";
            DateTime dateOfBirth = DateTime.Now;



            if (clsDatabasePeople.GetPersonByID(id, ref nationalno, ref firstName, ref secondName, ref thirdName, ref lastname, ref gendor, ref address, ref Phone,
            ref Email, ref nationalCountryID, ref imagepath, ref dateOfBirth) != false)
            {
                return new clsBuisnessPeople(id, nationalno, firstName, secondName, thirdName, lastname, gendor,
              address, Phone, Email, nationalCountryID, imagepath, dateOfBirth);
            }
            else
            {
                return null;
            }


        }

        static public clsBuisnessPeople Find(string nationalNo)
        {

            int nationalCountryID = 1, gendor = 0, id = -1;
            string firstName = "", secondName = "", thirdName = "", lastname = "", address = "", Email = "", Phone = "", imagepath = "";
            DateTime dateOfBirth = DateTime.Now;



            if (clsDatabasePeople.GetPersonByNationalNo(ref id, nationalNo, ref firstName, ref secondName, ref thirdName, ref lastname, ref gendor, ref address, ref Phone,
            ref Email, ref nationalCountryID, ref imagepath, ref dateOfBirth) != false)
            {
                return new clsBuisnessPeople(id, nationalNo, firstName, secondName, thirdName, lastname, gendor,
              address, Phone, Email, nationalCountryID, imagepath, dateOfBirth);
            }
            else
            {
                return null;
            }


        }


        static public DataTable GetallDataPeople(string subquery, string letters)
        {
            return clsDatabasePeople.GetAllDataFromPeople(subquery, letters);
        }


        static public bool FindNationalNo(string nationalNo)
        {

            return clsDatabasePeople.IsExists(nationalNo);

        }

        static public bool isExists(int id)
        {

            return clsDatabasePeople.IsExists(id);

        }


    }



}
