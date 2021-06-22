using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chechka.DAL.Entities
{
    public class ComputerPartGroup
    {
        public int ComputerPartGroupId { get; set; }
        public string GroupName { get; set; }
        /// <summary>
        /// Навигационное свойство 1-ко-многим
        /// </summary>
        public List<ComputerPart> ComputerParts { get; set; }


        //Lb6.4.6.1
        //public ComputerPartGroup()
        //{
        //    ComputerParts = new List<ComputerPart>();
        //}
    }
}
