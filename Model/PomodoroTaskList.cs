using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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
        public void initTest() {
            for (int i = 0; i < 10; i++)
                addPomodoro(new PomodoroClock("Appli .NET","EPSI",4,"test slt tag1 tag2"));
        }
        public void filter(string[] searchTag) {
            ObservableCollection<PomodoroClock> resultList = new ObservableCollection<PomodoroClock>();
            foreach(PomodoroClock p in pomodoroList) 
                if (p.tag.Intersect(searchTag).Count() == searchTag.Length)
                    resultList.Add(new PomodoroClock(p));
            //resultList.Add(new PomodoroClock(p));
            pomodoroList.Clear();
            foreach (PomodoroClock p in resultList)
                pomodoroList.Add(p);
        }
        public void addPomodoro(PomodoroClock pomodoro) {
            pomodoroList.Add(pomodoro);
            fullPomodoroList.Add(new PomodoroClock(pomodoro));
        }
        public void save() { /* TO DO save */ }
        public void load() { /* TO DO load */ }
    }
}
