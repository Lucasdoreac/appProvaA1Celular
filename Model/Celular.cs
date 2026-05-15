using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace appProvaA1Celular.Model
{
    [Table("Celular")]
    public class Celular : INotifyPropertyChanged
    {
        private int _celID;
        private string _celFabricante;
        private string _celModelo;

        [PrimaryKey, AutoIncrement, Unique, NotNull]
        public int celID
        {
            get => _celID;
            set
            {
                _celID = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(1000)]
        public string celFabricante
        {
            get => _celFabricante;
            set
            {
                _celFabricante = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(1000)]
        public string celModelo
        {
            get => _celModelo;
            set
            {
                _celModelo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
