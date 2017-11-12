using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.Tests
{
    public class ListRemoval_Test
    {

        private List<Data> dataList;

        public ListRemoval_Test() => dataList = new List<Data>();

        public void RemoveElements()
        {
            dataList.Add(new Data(new double[] { 0, 0 }, new double[] { 0, 69, 0, 0 }));
            dataList.Add(new Data(new double[] { 0, 1 }, new double[] { 1, 0, 0, 0 }));
            dataList.Add(new Data(new double[] { 0, 2 }, new double[] { 2, 69, 0, 0 }));
            dataList.Add(new Data(new double[] { 1, 0 }, new double[] { 3, 0, 0, 0 }));
            dataList.Add(new Data(new double[] { 0, 3 }, new double[] { 4, 0, 0, 0 }));
            dataList.Add(new Data(new double[] { 0, 0 }, new double[] { 5, 69, 0, 0 }));
            dataList.Add(new Data(new double[] { 0, 2 }, new double[] { 6, 69, 0, 0 }));
            dataList.Add(new Data(new double[] { 3, 0 }, new double[] { 7, 0, 0, 0 }));
            dataList.Add(new Data(new double[] { 4, 0 }, new double[] { 8, 0, 0, 0 }));
            dataList.Add(new Data(new double[] { 0, 6 }, new double[] { 9, 0, 0, 0 }));


            Console.WriteLine("List before: ");
            PrintList(dataList);

            var remove = new List<int>();
            dataList.Reverse();
            for (var j = 0; j < dataList.Count; j++)
            {
                for (var k = j + 1; k < dataList.Count; k++)
                {
                    if (dataList[j] == null || dataList[k] == null) break;
                    if (dataList[j].Values[0] == dataList[k].Values[0]
                        && dataList[j].Values[1] == dataList[k].Values[1])
                    {
                        var tmp = dataList[j];
                        var tmp2 = dataList[k];
                        var x_j = dataList[j].Values[0];
                        var x_k = dataList[k].Values[1];
                        var y_j = dataList[j].Values[0];
                        var y_k = dataList[k].Values[1];
                        remove.Add(k);
                    }
                }
            }
            

            foreach (var i in remove)
            {
                Console.WriteLine(i);
            }
            ;


            foreach (var indice in remove.OrderByDescending(v => v))
            {
                dataList.RemoveAt(indice);
            }

            dataList.Reverse();

            Console.WriteLine("List after: ");
            PrintList(dataList);
        }

        private static void PrintList(IEnumerable<Data> listOfData)
        {
            foreach (var data in listOfData)
            {
                Console.WriteLine(@"{0}, {1}: {2, 5}, {3, 5}, {4, 5}, {5, 5}", data.Values[0], data.Values[1],
                    data.Expectations[0], data.Expectations[1], data.Expectations[2], data.Expectations[3]);
            }
        }
    }
}