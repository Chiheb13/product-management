using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionProduit.ViewModels
{
    public class CreateProduitViewModel : INotifyPropertyChanged
    {
        private string _selectedCategory;
        private bool _isAccessoireChecked;
        private bool _isMobileChecked;
        private bool _isGamingChecked;
        private bool _isReseauChecked;
        private string _title;
        private string _description;
        private decimal _prix;
        private int _quantite;
        private DateTime _dateCreation;

        public bool IsAccessoireChecked
        {
            get => _isAccessoireChecked;
            set
            {
                _isAccessoireChecked = value;
                if (value)
                {
                    SelectedCategory = "Accessoire";
                }
                OnPropertyChanged();
            }
        }

        public bool IsMobileChecked
        {
            get => _isMobileChecked;
            set
            {
                _isMobileChecked = value;
                if (value)
                {
                    SelectedCategory = "Mobile";
                }
                OnPropertyChanged();
            }
        }

        public bool IsGamingChecked
        {
            get => _isGamingChecked;
            set
            {
                _isGamingChecked = value;
                if (value)
                {
                    SelectedCategory = "Gaming";
                }
                OnPropertyChanged();
            }
        }

        public bool IsReseauChecked
        {
            get => _isReseauChecked;
            set
            {
                _isReseauChecked = value;
                if (value)
                {
                    SelectedCategory = "Réseau";
                }
                OnPropertyChanged();
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public decimal Prix
        {
            get => _prix;
            set
            {
                _prix = value;
                OnPropertyChanged();
            }
        }

        public int Quantite
        {
            get => _quantite;
            set
            {
                _quantite = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateCreation
        {
            get => _dateCreation;
            set
            {
                _dateCreation = value;
                OnPropertyChanged();
            }
        }

        public CreateProduitViewModel()
        {
            // By default, set the first category to "Accessoire"
            IsAccessoireChecked = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
