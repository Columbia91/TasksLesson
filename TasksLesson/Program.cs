using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            // 1 способ
            Task task = new Task(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задача завершена");
            });
            // некоторое кол-во строк кода спустя

            task.Start();*/

            /*
            // 2 способ
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задача завершена");
                // опционально возвращение результата
                return 0;
            });*/

            /*
            // 3 способ
            var task = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задача завершена");
                // опционально возвращение результата
                return 0;
            });
            // только для получения результата
            var result = task.Result;*/

            // для отмены задачи
            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var cancellationTask = Task.Run(() =>
            {
                Thread.Sleep(200);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задача завершена");
                // опционально возвращение результата
                return 0;
            }, cancellationToken);

            cancellationSource.Cancel();
            Console.WriteLine("Главный поток завершил работу");
            Console.ReadLine();
        }

        private static Task DoSomething()
        {
            //return Task.Run(() => { }); - плохая идея
            return Task.CompletedTask; // полезно для async-await
        }

        private static Task<bool> DoSomethingWithBool()
        {
            //return Task.Run(() => { }); - плохая идея
            try
            {
                // для отмены Task.FromCancelled
                // передача результата можно и вне блока try
                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                // для исключения
                return Task.FromException<bool>(exception);
            }
        }
    }
}
