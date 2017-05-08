using OpenTK;
using System;
using System.Threading.Tasks;

namespace CmdRun.Engine.Rendering
{
    public class GameObject
    {
        /// <summary>
        /// Wird beim Zerstören dieses <see cref="GameObject"/>s aufgerufen
        /// </summary>
        public EventHandler<EventArgs> OnDestroyed;
        /// <summary>
        /// Wird nach der Initialisierung eines <see cref="GameObject"/>s aufgerufen
        /// </summary>
        public EventHandler<EventArgs> OnInitialized;

        /// <summary>
        /// Gibt den Mesh dieses <see cref="GameObject"/>s zurück
        /// </summary>
        public IShape Shape { get; private set; }
        /// <summary>
        /// Setzt die aktuelle Position dieses <see cref="GameObject"/>s oder gibt diese zurück
        /// </summary>
        public Vector2d Location { get; set; }
        /// <summary>
        /// Setzt die aktuelle Rotation dieses <see cref="GameObject"/>s oder gibt diese zurück
        /// </summary>
        public Quaterniond Rotation { get; set; }

        /// <summary>
        /// Führt die Initialisierung der Eigenschaften dieses <see cref="GameObject"/>s aus
        /// </summary>
        public void Initialize(IShape shape)
        {
            if(shape is null)
            {
                throw new NullReferenceException("Shape of GameObject cannot be \"null\"!");
            }
            Shape = shape;
            GameObjectRegister.Instance.Add(this);
            OnInitialized?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// Aktualisiert die Eingenschaften dieses <see cref="GameObject"/>s
        /// </summary>
        public void Update()
        {
            Shape?.Draw(Location, Rotation);
        }

        /// <summary>
        /// Zerstört dieses <see cref="GameObject"/>
        /// </summary>
        public void Destroy()
        {
            GameObjectRegister.Instance.Remove(this);
            OnDestroyed?.Invoke(this, new EventArgs());
            Shape = null;
            Location = Vector2d.Zero;
            Rotation = Quaterniond.Identity;
        }

        /// <summary>
        /// Zerstört dieses <see cref="GameObject"/>
        /// </summary>
        /// <param name="delay">Verzögerung der Zerstörung in Sekunden</param>
        public async void Destroy(double delay)
        {
            await Task.Delay((int)(1000 * delay));
            Destroy();
        }
    }
}
