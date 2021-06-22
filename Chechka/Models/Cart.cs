using Chechka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chechka.Models
{
    //Lb8.4.5.1{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity *
                item.Value.ComputerPart.Price);
            }
        }
        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="dish">добавляемый объект</param>
        public void AddToCart(ComputerPart computerPart)
        {

            // если объект есть в корзине
            // то увеличить количество

            if (Items.ContainsKey(computerPart.ComputerPartId))
                Items[computerPart.ComputerPartId].Quantity++;
            // иначе - добавить объект в корзину

            else
                Items.Add(computerPart.ComputerPartId, new CartItem
                {
                    ComputerPart = computerPart,
                    Quantity = 1
                });
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public void RemoveFromCart(int id)

        {
            Items.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public void ClearAll()
        {
            Items.Clear();
        }
    }
    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public ComputerPart ComputerPart { get; set; }
        public int Quantity { get; set; }
    }
    //Lb8.4.5.1}
}
