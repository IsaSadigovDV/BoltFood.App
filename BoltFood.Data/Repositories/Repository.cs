using BoltFood.Core.Models.Base;
using BoltFood.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private static List<T> _items = new List<T>();
        public List<T> Items { get { return _items; } }
        public async Task AddAsync(T model)
        {
            Items.Add(model);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return Items;
        }

        public async Task<T> GetAysnc(Func<T, bool> expression)
        {
            return Items.FirstOrDefault(expression);
        }

        public async Task RemoveAsync(T model)
        {
            Items.Remove(model);
        }

        public async Task UpdateAsync(T model)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (Items[i].Id == model.Id)
                {
                    _items[i] = model;
                }
            }
        }
    }
}
