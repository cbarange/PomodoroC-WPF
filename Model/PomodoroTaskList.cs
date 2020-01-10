using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EnvDotNetPomodoro.Model {
    public class PomodoroTaskList {
        public event EventHandler OnChange;
        public ObservableCollection<PomodoroClock> pomodoroList;

        public ObservableCollection<PomodoroClock> getList() {
            return pomodoroList;
        }
        
        public PomodoroTaskList() {
            pomodoroList = new ObservableCollection<PomodoroClock>();
            initTest();
        }
        public void initTest() {
            for (int i = 0; i < 10; i++)
                addPomodoro(new PomodoroClock("Appli .NET","EPSI",4,"test slt tag1 tag2"));
            return;
        }
        public void addPomodoro(PomodoroClock pomodoro) {
            pomodoroList.Add(pomodoro);
        }
        public void save() { /* TO DO save */ }
        public void load() { /* TO DO load */ }
    }
}
