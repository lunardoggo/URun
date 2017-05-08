using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdRun.Engine.Rendering
{
    public class GameObjectRegister : List<GameObject>
    {
#region Singleton pattern
        private static GameObjectRegister instance = null;

        public static GameObjectRegister Instance
        {
            get
            {
                if(instance is null)
                {
                    instance = new GameObjectRegister();
                }
                return instance;
            }
        }

        private GameObjectRegister()
        {

        }

#endregion

        public new void Add(GameObject gameObject)
        {
            if (!this.Contains(gameObject))
            {
                base.Add(gameObject);
            }
        }

        public void UpdateAll()
        {
            foreach(GameObject gameObject in this)
            {
                gameObject.Update();
            }
        }
    }
}
