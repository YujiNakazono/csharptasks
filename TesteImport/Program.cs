using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TesteImport {
    //==========================================================================
    public class Program {

        #region Variables
        //----------------------------------------------------------------------
        public static List<Task> Tasks { get; set; }
        //----------------------------------------------------------------------
        public static string path = "C:\\teste";
        //----------------------------------------------------------------------
        #endregion

        #region Methods
        //----------------------------------------------------------------------
        static void Main(string[] args) {
            Tasks = new List<Task>();
            var infoP = new DirectoryInfo(path);
            var files = infoP.GetFiles();
            foreach(var file in files) {
                var info = file;
                Tasks.Add(Task.Factory.StartNew(async () => {
                    await TarefaLonga(info);
                }));
            }
            var a = Execute().Result;
            Console.ReadKey();
        }
        //----------------------------------------------------------------------
        private static async Task<bool> Execute() {
            await Task.WhenAll(Tasks);
            return true;
        }
        //----------------------------------------------------------------------
        public static async Task<bool> TarefaLonga(FileInfo info) {
            await Task.Delay(1000);

            using(var sw = info.AppendText()) {
                await sw.WriteLineAsync(info.Name);
            }
            Console.WriteLine(info.Name);
            return true;
        }
        //----------------------------------------------------------------------
        #endregion
        
    }
    //==========================================================================
}
