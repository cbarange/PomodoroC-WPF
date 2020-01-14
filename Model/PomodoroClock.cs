using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace EnvDotNetPomodoro.Model
{
    [Serializable()]
    public class PomodoroClock : INotifyPropertyChanged {
        // --- REAL TIMER ---
        /*
        private const int CLOCK_TIMER_WORK = 25 * 60 * 1000;
        private const int CLOCK_TIMER_WORK = 25 * 60 * 1000;
        private const int CLOCK_TIMER_WORK = 25 * 60 * 1000;
        */
        // --- DEBUG TIMER ---
        private const int CLOCK_TIMER_WORK = 25 * 60 * 10;
        private const int CLOCK_TIMER_BREAK = 5 * 60 * 10;
        private const int CLOCK_TIMER_PAUSE = 15 * 60 * 10;
        public CountDownTimer countDownTimer { get; set; }
        public int[] config;
        private PomodoroClock p;

        private int _currentIndexTimer;
        [field: NonSerialized]
        public event EventHandler OnChange;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public int currentIndexTimer { 
            get { return _currentIndexTimer; } 
            set {
                _currentIndexTimer = value;
                if (OnChange != null) { OnChange(this, new EventArgs()); } 
            }
        }
        public int priorite { get; set; }
        public string sujet { get; set; }
        public string client { get; set; }
        public string[] tag { get; set; }
        public string date { get; set; }
        public string tagAsString{ get { return string.Join(" #", tag); } }

        public PomodoroClock(string sujet,string client, int priorite, string tags, string date) {
            config = new int[]{CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_PAUSE};
            currentIndexTimer = 0;
            this.sujet = sujet;
            this.client = client;
            this.priorite = priorite;
            tag = tags.Split(' ');
            countDownTimer = new CountDownTimer(config[currentIndexTimer]);
            this.date = date;
        }

        public PomodoroClock(PomodoroClock p) {
            config = new int[] { CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_BREAK, CLOCK_TIMER_WORK, CLOCK_TIMER_PAUSE };
            currentIndexTimer = 0;
            this.sujet = p.sujet;
            this.client = p.client;
            this.priorite = p.priorite;
            tag = p.tag;
            countDownTimer = new CountDownTimer(p.countDownTimer);
            this.date = p.date;
        }

        public void start() { countDownTimer.startTimer(); }
        public void stop() { countDownTimer.stopTimer(); }
        public void play() { countDownTimer.playTimer(); }
        public void pause() { countDownTimer.pauseTimer(); }
        public void next() {
            currentIndexTimer++;
            if (currentIndexTimer > 7) { 
                countDownTimer.stopTimer();
                List<string> newTag = new List<string>();
                foreach (string s in tag)
                    newTag.Add(s);
                newTag.Add("finish");
                this.tag = newTag.ToArray();
            } else 
                countDownTimer.nextTimer(config[currentIndexTimer]);
        }
        public override string ToString() {
            return sujet + client + tagAsString + priorite + date + currentIndexTimer.ToString();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            throw new NotImplementedException();
        }
    }
    [Serializable()]
    public class CountDownTimer {
        private int? _timer;
        private Boolean start = false;
        private CountDownTimer countDownTimer;

        [field: NonSerialized]
        public event EventHandler OnChange;
        public int? timer {
            get { return _timer; }
            set { 
                _timer = value;
                if (OnChange!=null) {
                    OnChange(this, new EventArgs());
                }            
            }
        }
        public CountDownTimer(int delay) {
            timer = delay;
            Thread t = new Thread(new ThreadStart(tikeTimer));
            t.Start();
        }

        public CountDownTimer(CountDownTimer countDownTimer) {
            this.timer = countDownTimer.timer;
            this.start = countDownTimer.start;
            Thread t = new Thread(new ThreadStart(tikeTimer));
            t.Start();
        }

        public void startTimer() {
            start = true;
        }
        public void tikeTimer() {
            while (timer>0) {
                if (start)
                    timer-=1000;
                Thread.Sleep(1000);
            }
        }
        public void stopTimer() {
            start = false;
            timer = 0;
        }
        public void pauseTimer() {
            start = false;
        }
        public void playTimer() {
            start = true;
        }
        public void nextTimer(int delay) {
            timer = delay;
            start = true;
        }
        public string getTimerValue() {
            int? seconde = (timer / 1000) % 60;
            int? minute = ((timer - seconde)/1000) / 60;
            string sminute = minute.ToString().Length < 2 ? '0' + minute.ToString() : minute.ToString();
            string sseconde= seconde.ToString().Length < 2 ? '0' + seconde.ToString() : seconde.ToString();
            return sminute+':'+sseconde;
        }
        public Boolean finish() {
            if (timer < 0)
                return true;
            return false;
        }
        
    }
}
