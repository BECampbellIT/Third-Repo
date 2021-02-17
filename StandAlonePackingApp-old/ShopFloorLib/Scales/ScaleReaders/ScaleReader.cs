using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloorLib
{
    public abstract class ScaleReader
    {
        public enum Stability { stableWeight, unknown, unstableWeight };
        public enum ScaleCommsType { serial, tcp, none };

        public class ScaleInfo
        {
            public string scaleType;
            public ScaleCommsType commsType;
            public string description;
            public Type objType;
        }

        protected bool running;
        public ScaleSettings settings { private set; get; }

        public ScaleReader()
        {
        }
        public virtual void Open()
        {
            running = true;

            if (settings.localTare)
                SetTare(0.001m);
        }
        public virtual void Close()
        {
            running = false;
        }

        public abstract ScaleInfo GetScaleTypeInfo();

        public abstract string PollScaleWeight();
        public abstract void InterpretResult(string res, out decimal gross, out decimal net, out decimal tare, out Stability stab);
        public abstract void SetTare(decimal newTare);

        public static ScaleReader GetScaleReader()
        {
            var s = (ScaleSettings)XMLConfigHandler.ReadConfig(typeof(ScaleSettings));
            
            var scaleTypes = GetScaleTypes();
            var scaleInfo = scaleTypes.Find(si=>si.scaleType.Equals(s.scaleType));
            var sr = (ScaleReader)Activator.CreateInstance(scaleInfo.objType);
            sr.settings = s;
            return sr;
        }

        public static List<ScaleReader.ScaleInfo> GetScaleTypes()
        {
            var scaleTypes = new List<ScaleReader.ScaleInfo>();
            var baseType = typeof(ScaleReader);

            var types = baseType.Assembly.GetTypes().Where(t => t.BaseType == baseType);

            foreach (var t in types)
            {
                var method = t.GetMethod("GetScaleTypeInfo");
                var instance = Activator.CreateInstance(t);
                var scaleInfo = (ScaleReader.ScaleInfo)method.Invoke(instance, null);
                scaleInfo.objType = t;
                scaleTypes.Add(scaleInfo);
            }
            return scaleTypes;
        }
    }
}
