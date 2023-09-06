using System.Collections.Generic;
using Godot;

namespace Farmer.Scripts.Main.Game.Database
{
    public class ItemDatabase: Node
    {
        private static ItemDatabase _instance;
        public static ItemDatabase Instance
        {
            get { return _instance; }
        }

        ItemDatabase()
        {
            _instance = this;
            // GD.Print("Itemdatabase AUTOLOAD");
        }

        public Node GetMainNode()
        {
            Node root = GetTree().Root;
            return root.GetChild(root.GetChildCount() - 1);
        }
        
        public List<ItemResource> items = new List<ItemResource>();
        // change to Godot Dictionary

        public override void _Ready()
        {
            LoadResources();
        }

        private void LoadResources()
        {
            var directory = new Directory();
            directory.Open("res://Items");
            if (!directory.DirExists("res://Items"))
            {
                GD.Print("Directory not found");
                return;
            }
            directory.ListDirBegin(true, true);

            string filename = directory.GetNext();
            int id = 0;

            while (directory.FileExists(filename))
            {
                if (!directory.CurrentIsDir())
                {
                    // GD.Print("start loading");
                    ItemResource item = (ItemResource) GD.Load("res://Items/" + filename);
                    items.Add(item);
                    item.itemId = id;
                    // GD.Print(item.itemId);
                    ++id;
                }

                filename = directory.GetNext();
            }
        }

        public ItemResource GetItem(string itemName)
        {
            foreach (var item in items)
            {
                if (item.itemName == itemName)
                {
                    return item;
                }
                

            }
            return null;
        }
        
        public ItemResource GetItem(int itemId)
        {
            foreach (var item in items)
            {
                if (item.itemId == itemId)
                {
                    return item;
                }
                

            }
            return null;
        }
    }
}