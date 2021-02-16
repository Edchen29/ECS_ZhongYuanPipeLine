using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HHECS.Model.Entities
{
    [Serializable]
    public class BaseModel : INotifyPropertyChanged, ICloneable
    {
        [Key]
        public int? Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void HandlerPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        /// <summary>
        /// 实现ICloneable接口，达到浅表复制。
        /// 浅表复制会复制出一个新对象，新对象的值类型会复制一份新的，但是对象包含的引用类型依然是旧的引用。
        /// </summary>
        /// <returns></returns>
        public Object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 深度复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public Object DeepClone()
        {
            // Don't serialize a null object, simply return the default for that object
            if (this == null)
            {
                return null;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream);
            }
        }
    }
}
