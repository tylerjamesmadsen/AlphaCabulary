using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace AlphaCabulary.Models
{
    public class ItemRepository : IItemRepository
    {
        private static ConcurrentDictionary<string, Item> _items =
            new ConcurrentDictionary<string, Item>();

        public ItemRepository()
        {
            Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 1", Description = "This is an item description." });
            Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 2", Description = "This is an item description." });
            Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 3", Description = "This is an item description." });
        }

        public Item Get(string id)
        {
            return _items[id];
        }

        public IEnumerable<Item> GetAll()
        {
            return _items.Values;
        }

        public void Add(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            _items[item.Id] = item;
        }

        public Item Find(string id)
        {
            Item item;
            _items.TryGetValue(id, out item);

            return item;
        }

        public Item Remove(string id)
        {
            Item item;
            _items.TryRemove(id, out item);

            return item;
        }

        public void Update(Item item)
        {
            _items[item.Id] = item;
        }
    }
}
