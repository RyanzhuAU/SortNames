using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SortPersonList
{
    class Program
    {
        /// <summary>
        ///  Demo program for the GlobalX test.
        ///  Read the list of name from the text file. Sort the name list by last name and first name. Then output the new name list.
        /// </summary>
        static void Main(string[] args)
        {
            string line;
            List<Person> personList = new List<Person>();

            // Read the file.
            string path = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), @"Data\names.txt");
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            
            //Loop the file and put names into a list.
            while ((line = file.ReadLine()) != null)
            {
                List<string> names = line.Split(',').ToList<string>();

                Person person = new Person();
                person.FirstName = names[1];
                person.LastName = names[0];

                personList.Add(person);
            }

            file.Close();

            //Sort the name list with Lastname and Firstname.
            personList.Sort(
                delegate(Person p1, Person p2)
                {
                    int compareDate = p1.LastName.CompareTo(p2.LastName);
                    if (compareDate == 0)
                    {
                        return p1.FirstName.CompareTo(p2.FirstName);
                    }
                    return compareDate;
                }
            );

            //Generate the new file for the sorted name list.
            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(@".\newNames.txt"))
            {
                foreach (Person person in personList)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(person.LastName);
                    sb.Append(",");
                    sb.Append(person.FirstName);

                    outputFile.WriteLine(sb.ToString());
                }
            }

        }
    }
}
