﻿using appReclamos.Helpers;
using appReclamos.Models;
using appReclamos.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace appReclamos.ViewModels
{
    public class GetViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string idreclamo;
        private string idcliente;
        private string idempleado;
        private string idorden;
        private string tipo;
        private string asunto;
        private string descripcion;
        private string estado;
        private string causa;
        private string[] respuestaslist;
        

        #endregion

        #region Properties
        public string IdReclamo
        {
            get { return this.idreclamo; }
            set { SetValue(ref this.idreclamo, value); }
        }
        public string IdCliente 
        {
            get { return this.idcliente; }
            set { SetValue(ref this.idcliente, value); }

        }
        public string IdEmpleado 
        {
            get { return this.idempleado; }
            set { SetValue(ref this.idempleado, value); }
        }
        public string IdOrden
        {
            get { return this.idorden; }
            set { SetValue(ref this.idorden, value); }
        }
        public string Tipo
        {
            get { return this.tipo; }
            set { SetValue(ref this.tipo, value); }
        }
        public string Asunto
        {
            get { return this.asunto; }
            set { SetValue(ref this.asunto, value); }
        }
        public string Descripcion
        {
            get { return this.descripcion; }
            set { SetValue(ref this.descripcion, value); }
        }
        public string Estado
        {
            get { return this.estado; }
            set { SetValue(ref this.estado, value); }
        }
        public string Causa
        {
            get { return this.causa; }
            set { SetValue(ref this.causa, value); }
        }
        public string[] RespuestasList
        {
            get { return this.respuestaslist; }
            set { SetValue(ref this.respuestaslist, value); }
        }
        #endregion

     


        #region Constructor
        public GetViewModel(string id)
        {
            this.apiService = new ApiService();
            this.IdReclamo = id;
            loadSupports(this.IdReclamo);

        }

        public async void loadSupports(string id)
        {

            var response = await this.apiService.GetList<Support>(
                "https://getsupportsyn.azurewebsites.net/",
                "api/",
                "FGetSupport/"+id
                
                ) ;
            
           

            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Success",
                   "Se ha Encontrado correctamente",
                   "Aceptar");
                var data = (List<Support>)response.Result;
                
                this.IdCliente = data[0].ClientId;
                this.IdEmpleado = data[0].EmployeeId;
                this.IdOrden = data[0].OrderId;
                this.Tipo = data[0].Type;
                this.Asunto = data[0].Subject;
                this.Descripcion = data[0].Description;
                this.Estado = data[0].Status;
                this.Causa = data[0].Cause;
                this.RespuestasList = data[0].Answers;

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   response.Message,
                   "Aceptar");
            }

        }

        public async void ModifyAnswer()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Modify = new ModifyViewModel(this.IdReclamo);
            await Application.Current.MainPage.Navigation.PushAsync(new ModifyPage(this.IdReclamo));

        }
        
        public async void DeleteSupport()
        {
            
            var response = await this.apiService.Delete<Support>(
                  
                "https://deletesupportsyn.azurewebsites.net/",
                "api/",
                "FDeleteSupport/" + this.IdReclamo 
                
                );
            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Success",
                   "Se ha Eliminado  correctamente",
                   "Aceptar");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Se produjo un error",
                   "Aceptar");
            }
        }

        #endregion

        #region Commands
        public ICommand AnswerCommand
        {
            get
            {
                return new RelayCommand(ModifyAnswer);
            }

        }
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteSupport);
            }

        }




        #endregion
    }
}
