using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.GeneralHelpers
{
    public class Remover
    {
        public static List<Data> RemoveSameElementsInDataList(List<Data> dataList)
        {
            var whichToRemove = new List<int>();

            dataList.Reverse();

            for (var j = 0; j < dataList.Count; j++)
            {
                for (var k = j + 1; k < dataList.Count; k++)
                {
                    if (dataList[j].Values[0] == dataList[k].Values[0] && dataList[j].Values[1] == dataList[k].Values[1])
                        whichToRemove.Add(k);
                }
            }

            
            foreach (var indice in whichToRemove.OrderByDescending(v => v))
            {
                dataList.RemoveAt(indice);
            }

            dataList.Reverse();

            return dataList;
        }
    }
}