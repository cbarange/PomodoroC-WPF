using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace EnvDotNetPomodoro.Model {
    public class PomodoroTaskList {
        public event EventHandler OnChange;
        public ObservableCollection<PomodoroClock> pomodoroList;
        public ObservableCollection<PomodoroClock> fullPomodoroList;
        public PomodoroTaskList() {
            fullPomodoroList = new ObservableCollection<PomodoroClock>();
            pomodoroList = new ObservableCollection<PomodoroClock>();
            initTest();
        }
        public string RandomString(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void initTest() {
            for (int i = 0; i < 10; i++)
                addPomodoro(new PomodoroClock(RandomString(6), RandomString(6), 4,"tag1 tag2 "+ RandomString(4),"30/02/2020"));
        }
        public void filter(string[] searchTag) {
            ObservableCollection<PomodoroClock> resultList = new ObservableCollection<PomodoroClock>();
            foreach (PomodoroClock p in pomodoroList)
                if (p.tag.Intersect(searchTag).Count() == searchTag.Length || searchTag.Contains(p.date))
                    resultList.Add(new PomodoroClock(p));
            //resultList.Add(new PomodoroClock(p));
            pomodoroList.Clear();
            foreach (PomodoroClock p in resultList)
                pomodoroList.Add(p);
        }

        public void filterPriorite(int prio) {
            ObservableCollection<PomodoroClock> resultList = new ObservableCollection<PomodoroClock>();
            foreach (PomodoroClock p in fullPomodoroList)
                if (p.priorite >= prio)
                    resultList.Add(new PomodoroClock(p));
            pomodoroList.Clear();
            foreach (PomodoroClock p in resultList)
                pomodoroList.Add(p);
        }
        public void resetFilter() {
            foreach (PomodoroClock p in pomodoroList) {
                Boolean isInList = false;
                foreach (PomodoroClock p2 in fullPomodoroList)
                    if (p.ToString() == p2.ToString())
                        isInList = true;
                if (!isInList)
                    fullPomodoroList.Add(p);

            }

            pomodoroList.Clear();
            foreach (PomodoroClock p in fullPomodoroList)
                pomodoroList.Add(p);
        }
        public void addPomodoro(PomodoroClock pomodoro) {
            pomodoroList.Add(pomodoro);
            fullPomodoroList.Add(new PomodoroClock(pomodoro));
        }
        public string save() {
            string fileName = @"dataPomodoro.data";
            if (File.Exists(fileName))
                File.Delete(fileName);
            try {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, fullPomodoroList);
                stream.Close();
            } catch (Exception e) { 
                return e.Message;
            }
            return "ok";
        }
        public string load() {
            string fileName = @"dataPomodoro.data";
            try {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                ObservableCollection<PomodoroClock> serializePomodoroList = (ObservableCollection<PomodoroClock>)formatter.Deserialize(stream);
                fullPomodoroList.Clear();
                foreach (PomodoroClock p in serializePomodoroList)
                    fullPomodoroList.Add(new PomodoroClock(p));
                pomodoroList.Clear();
                foreach (PomodoroClock p in fullPomodoroList)
                    pomodoroList.Add(p);
            } catch (Exception e) {
                return e.Message;
            }
            return "ok";
        }
    }
}
