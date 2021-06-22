using Chechka.DAL.Data;
using Chechka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chechka.Tests
{
    class TestData
    {
        public static List<ComputerPart> GetComputerPartsList()
        {
            return new List<ComputerPart>
                {
                    new ComputerPart{ ComputerPartId=1, /*Lb6.4.7.1{*/ ComputerPartGroupId=1/*}Lb6.4.7.1*/},
                    new ComputerPart{ ComputerPartId=2, /*Lb6.4.7.1{*/ ComputerPartGroupId=1/*}Lb6.4.7.1*/},
                    new ComputerPart{ ComputerPartId=3, /*Lb6.4.7.1{*/ ComputerPartGroupId=1/*}Lb6.4.7.1*/},
                    new ComputerPart{ ComputerPartId=4, /*Lb6.4.7.1{*/ ComputerPartGroupId=2/*}Lb6.4.7.1*/},
                    new ComputerPart{ ComputerPartId=5, /*Lb6.4.7.1{*/ ComputerPartGroupId=2/*}Lb6.4.7.1*/},
                    new ComputerPart{ ComputerPartId=6, /*Lb6.4.7.1{*/ ComputerPartGroupId=2/*}Lb6.4.7.1*/}
                };
        }
        public static IEnumerable<object[]> Params()
        {
            // 1-я страница, кол. объектов 3, id первого объекта 1
            yield return new object[] { 1, 3, 1 };
            // 2-я страница, кол. объектов 2, id первого объекта 4
            yield return new object[] { 2, 3, 4 };
        }

        //Lb8.4.3.1{
        public static void FillContext(ApplicationDbContext context)
        {
            context.ComputerPartGroups.Add(new ComputerPartGroup
            { GroupName = "fake group" });
                        context.AddRange(new List<ComputerPart>
            {
                    new ComputerPart{ ComputerPartId=1,ComputerPartGroupId=1},
                    new ComputerPart{ ComputerPartId=2,ComputerPartGroupId=1},
                    new ComputerPart{ ComputerPartId=3,ComputerPartGroupId=1},
                    new ComputerPart{ ComputerPartId=4,ComputerPartGroupId=2},
                    new ComputerPart{ ComputerPartId=5,ComputerPartGroupId=2},
                    new ComputerPart{ ComputerPartId=6,ComputerPartGroupId=2}
            });
            context.SaveChanges();
        }
        //Lb8.4.3.1}
    }
}
