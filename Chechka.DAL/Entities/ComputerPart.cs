using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chechka.DAL.Entities
{
    public class ComputerPart
    {
        public int ComputerPartId { get; set; }        //id Комплектующего
        public string ComputerPartName { get; set; }   //Название
        public string Description { get; set; }         //Описание
        public int Price { get; set; }              //Цена
        public string Image { get; set; }               //Имя файла изображения

        // Навигационные свойства
        /// <summary>
        /// группа коплектующих (процессор, видеокарта и т.д.)
        /// </summary>
        public int ComputerPartGroupId { get; set; }
        public ComputerPartGroup Group { get; set; }
    }
}