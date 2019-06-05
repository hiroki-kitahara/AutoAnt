using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Internal
{
    /// <summary>
    /// 1個単位のセーブデータ
    /// </summary>
    public sealed class SaveData<T> : ISaveData<T>
    {
        private readonly string key;

        private readonly string filePath;

        private readonly ES3Settings settings;

        public SaveData(string key)
            : this(key, string.Empty, new ES3Settings())
        {
        }

        public SaveData(string key, ES3Settings settings)
            : this(key, string.Empty, settings)
        {
        }

        public SaveData(string key, string filePath)
            : this(key, filePath, new ES3Settings())
        {
        }

        public SaveData(string key, string filePath, ES3Settings settings)
        {
            this.key = key;
            this.filePath = filePath;
            this.settings = settings;
        }

        void ISaveData<T>.Save(T value)
        {
            if(string.IsNullOrEmpty(this.filePath))
            {
                ES3.Save<T>(this.key, value, this.settings);
            }
            else
            {
                ES3.Save<T>(this.key, value, this.filePath, this.settings);
            }
        }

        T ISaveData<T>.Load()
        {
            if(string.IsNullOrEmpty(this.filePath))
            {
                return ES3.Load<T>(this.key, this.settings);
            }
            else
            {
                return ES3.Load<T>(this.key, this.filePath, this.settings);
            }
        }

        T ISaveData<T>.Load(T ifNotExistsValue)
        {
            if (string.IsNullOrEmpty(this.filePath))
            {
                return ES3.Load<T>(this.key, ifNotExistsValue, this.settings);
            }
            else
            {
                return ES3.Load<T>(this.key, this.filePath, ifNotExistsValue, this.settings);
            }
        }

        bool ISaveData<T>.Exists()
        {
            if(string.IsNullOrEmpty(this.filePath))
            {
                return ES3.KeyExists(this.key, this.settings);
            }
            else
            {
                return ES3.KeyExists(this.key, this.filePath, this.settings);
            }
        }
    }
}
