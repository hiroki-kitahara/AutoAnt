using HK.AutoAnt.Systems;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 街の人口を加算するインターフェイス
    /// </summary>
    public interface IAddTownPopulation
    {
        /// <summary>
        /// 人口を加算する
        /// </summary>
        void Add(float deltaTime);
    }
}
