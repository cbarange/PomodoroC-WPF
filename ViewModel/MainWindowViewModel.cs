﻿using EnvDotNetPomodoro.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace EnvDotNetPomodoro {

    public class MainWindowViewModel : SimpleObservableObject {
        // --- ============== DEBUG ============= ---
        private void DoStuff(PomodoroClock item) { MessageBox.Show(item.sujet + " element clicked"); }

        // --- ================================== ---
        public event PropertyChangedEventHandler PropertyChanged;
        // --- Slider priority ---
        private int _prioriteSlider;
        public int PrioriteSlider { get { return _prioriteSlider; } set { PrioriteSlider_OnChange(); Set(ref _prioriteSlider, value); } }
        // --- Search by Tag ---
        private string _searchTag;
        public string SearchTag { get { return _searchTag; } set { Set(ref _searchTag, value); } }
        // --- Add pomodoro ---
        private Visibility _stackPanelVisibility;
        public Visibility StackPanelVisibility { get { return _stackPanelVisibility; } set { Set(ref _stackPanelVisibility, value); } }
        private string _newPomodoroSujet;
        public string newPomodoroSujet { get { return _newPomodoroSujet; } set { Set(ref _newPomodoroSujet, value); } }
        private string _newPomodorotags;
        public string newPomodorotags { get { return _newPomodorotags; } set { Set(ref _newPomodorotags, value); } }
        private int _newPomodoroPriorite;
        public int newPomodoroPriorite { get { return _newPomodoroPriorite; } set { Set(ref _newPomodoroPriorite, value); } }
        private string _newPomodoroClient;
        public string newPomodoroClient { get { return _newPomodoroClient; } set { Set(ref _newPomodoroClient, value); } }
        // --- Current pomodoro ---
        private string? _pomodoroSujet;
        public string? PomodoroSujet { get { return _pomodoroSujet; } set { Set(ref _pomodoroSujet, value); } }
        // --- Pomodoro task list ---
        private PomodoroTaskList _pomodoroTaskList;
        public ObservableCollection<PomodoroClock> PomodoroTaskList { get { return _pomodoroTaskList.pomodoroList; } }
        // --- Timer ---
        private string? _timerPomodoro;
        public string? TimerPomodoro { get { return _timerPomodoro; } set { Set(ref _timerPomodoro, value); } }
        private PomodoroClock monTimer { get; set; }

        // --- Click in ListView ---
        private ICommand _clickListView;
        public ICommand ClickListView { get {
                return _clickListView ?? (_clickListView = new RelayCommand(x => {changeCurrentPomodoro(x as PomodoroClock); DoStuff(x as PomodoroClock); })); } }
        // --- Click Recherche ---
        private ICommand _clickRecherche;
        public ICommand ClickRechercher{
            get { return _clickRecherche ?? (_clickRecherche = new CommandHandler(() => ClickOnRechercher(), () => CanExecute)); } }
        // --- Click Ajouter ---
        private ICommand _clickAjouter;
        public ICommand ClickAjouter {
            get { return _clickAjouter ?? (_clickAjouter = new CommandHandler(() => ClickOnAjouter(), () => CanExecute)); } }
        // --- Click valider ajout ---
        private ICommand _clickValider;
        public ICommand ClickValider {
            get { return _clickValider ?? (_clickValider = new CommandHandler(() => ClickOnValider(), () => CanExecute)); }}
        // --- Click annuler ajout ---
        private ICommand _clickAnnuler;
        public ICommand ClickAnnuler {
            get { return _clickAnnuler ?? (_clickAnnuler = new CommandHandler(() => ClickOnCancel(), () => CanExecute)); }
        }
        // --- Click Start ---
        private ICommand _clickStart;
        public ICommand ClickStart {
            get { return _clickStart ?? (_clickStart = new CommandHandler(() => ClickOnStart(), () => CanExecute)); } }
        // --- Click Stop ---
        private ICommand _clickStop;
        public ICommand ClickStop {
            get { return _clickStop ?? (_clickStop = new CommandHandler(() => ClickOnStop(), () => CanExecute)); } }
        // --- Click Play ---
        private ICommand _clickPlay;
        public ICommand ClickPlay{
            get { return _clickPlay ?? (_clickPlay = new CommandHandler(() => ClickOnPlay(), () => CanExecute)); } }
        // --- Click Pause ---
        private ICommand _clickPause;
        public ICommand ClickPause {
            get { return _clickPause ?? (_clickPause = new CommandHandler(() => ClickOnPause(), () => CanExecute)); } }
        // --- Click Next ---
        private ICommand _clickNext;
        public ICommand ClickNext{
            get { return _clickNext ?? (_clickNext = new CommandHandler(() => ClickOnNext(), () => CanExecute)); } }

        
        public MainWindowViewModel() {
            _pomodoroTaskList = new PomodoroTaskList();
            monTimer = _pomodoroTaskList.pomodoroList[0];
            monTimer.countDownTimer.OnChange += MonTimer_OnChange;
            StackPanelVisibility = Visibility.Hidden;
        }
        public event EventHandler OnChange;
        private void changeCurrentPomodoro(PomodoroClock p) {
            monTimer.pause();
            monTimer = p;

        }
        private void PrioriteSlider_OnChange() {
            _pomodoroTaskList.filterPriorite(PrioriteSlider);
        }
        private void MonTimer_OnChange(object sender, EventArgs e) {
            if (monTimer.countDownTimer.finish())
                monTimer.next();
            TimerPomodoro = monTimer.countDownTimer.getTimerValue();
            PomodoroSujet = monTimer.sujet + " / " + monTimer.client;
        }
        // --- Click on search by tag ---
        public void ClickOnRechercher() {
            if (SearchTag != "" && SearchTag != null) {
                string[] searchedTag = SearchTag.Split(' ');
                _pomodoroTaskList.filter(searchedTag);
            } else {
                _pomodoroTaskList.resetFilter();
            }
        }
        // --- Click on add pomodoro ---
        public void ClickOnAjouter() { StackPanelVisibility = Visibility.Visible; }
        public void ClickOnValider() { 
            StackPanelVisibility = Visibility.Hidden;
            _pomodoroTaskList.addPomodoro(new PomodoroClock(newPomodoroClient, newPomodoroSujet, newPomodoroPriorite, newPomodorotags));
        }
        public void ClickOnCancel() { StackPanelVisibility = Visibility.Hidden; }
        // --- Click on Start/Stop/Pause/Play ---
        public bool CanExecute { get { return true; } }
        public void ClickOnStart() { monTimer.start(); }
        public void ClickOnStop() { monTimer.stop(); }
        public void ClickOnPlay() { monTimer.play(); }
        public void ClickOnPause() { monTimer.pause(); }
        public void ClickOnNext() { monTimer.next(); }
    }



    public class SimpleObservableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class CommandHandler : ICommand {
        private Action _action;
        private Func<bool> _canExecute;
        public CommandHandler(Action action, Func<bool> canExecute) {
            _action = action;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter) { return _canExecute.Invoke(); }
        public void Execute(object parameter) { _action(); }
    }

    public class EventArgs<T> : EventArgs {
        public EventArgs(T value) { Value = value; }
        public T Value { get; private set; }
    }

    public static class EventRaiser {
        public static void Raise(this EventHandler handler, object sender) { handler?.Invoke(sender, EventArgs.Empty); }
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T value) { handler?.Invoke(sender, new EventArgs<T>(value));}
        public static void Raise<T>(this EventHandler<T> handler, object sender, T value) where T : EventArgs { handler?.Invoke(sender, value); }
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, EventArgs<T> value) { handler?.Invoke(sender, value); }
    }
    public class RelayCommand<T> : ICommand {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
           : this(execute, null) {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute) {
            if (execute == null) {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter) {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand : ICommand {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        public RelayCommand(Action<object> execute) : this(execute, null) { _execute = execute; }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) { return _canExecute == null || _canExecute(parameter); }
        public void Execute(object parameter) { _execute(parameter); }
        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        public event EventHandler CanExecuteChanged {
            add {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        private event EventHandler CanExecuteChangedInternal;

        public void RaiseCanExecuteChanged() { CanExecuteChangedInternal.Raise(this); }
    }
}
