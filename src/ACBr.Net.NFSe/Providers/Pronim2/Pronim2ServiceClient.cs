﻿// ***********************************************************************
// Assembly         : ACBr.Net.NFSe
// Author           : Felipe Silveira/Transis
// Created          : 14-02-2020
//
// ***********************************************************************
// <copyright file="Pronim2ServiceClient.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2020 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;

namespace ACBr.Net.NFSe.Providers
{
    internal sealed class Pronim2ServiceClient : NFSeSOAP11ServiceClient, IABRASF2Client
    {
        #region Constructors

        public Pronim2ServiceClient(ProviderPronim2 provider, TipoUrl tipoUrl, X509Certificate2 certificado) : base(provider, tipoUrl, certificado)
        {
        }

        public Pronim2ServiceClient(ProviderPronim2 provider, TipoUrl tipoUrl) : base(provider, tipoUrl)
        {
        }

        #endregion Constructors

        #region Methods

        public string CancelarNFSe(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:CancelarNfse>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:CancelarNfse>");

            return Execute("CancelarNfse", message.ToString(), "CancelarNfseResponse");
        }

        public string SubstituirNFSe(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:SubstituirNfse>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:SubstituirNfse>");

            return Execute("SubstituirNfse", message.ToString(), "SubstituirNfseResponse");
        }

        public string ConsultarLoteRps(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:ConsultarLoteRps>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:ConsultarLoteRps>");

            return Execute("ConsultarLoteRps", message.ToString(), "ConsultarLoteRpsResponse");
        }

        public string ConsultarNFSePorRps(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:ConsultarNfsePorRps>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:ConsultarNfsePorRps>");

            return Execute("ConsultarNfsePorRps", message.ToString(), "ConsultarNfsePorRpsResponse");
        }

        public string ConsultarNFSeServicoPrestado(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:ConsultarNfseServicoPrestado>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:ConsultarNfseServicoPrestado>");

            return Execute("ConsultarNfseServicoPrestado", message.ToString(), "ConsultarNfseServicoPrestadoResponse");
        }

        public string ConsultarNFSeFaixa(string cabec, string msg)
        {
            //NAO EXISTE NO PRONIM
            return "";
        }

        public string ConsultarNFSeServicoTomado(string cabec, string msg)
        {
            //NAO EXISTE NO PRONIM
            return "";
        }        

        public string RecepcionarLoteRps(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:RecepcionarLoteRps>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:RecepcionarLoteRps>");

            return Execute("RecepcionarLoteRps", message.ToString(), "RecepcionarLoteRpsResponse");
        }

        public string RecepcionarLoteRpsSincrono(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<tem:EnviarLoteRpsSincrono>");
            message.Append("<tem:xmlEnvio>");
            message.AppendCData(msg);
            message.Append("</tem:xmlEnvio>");
            message.Append("</tem:EnviarLoteRpsSincrono>");

            return Execute("EnviarLoteRpsSincrono", message.ToString(), "EnviarLoteRpsSincronoResponse");
        }

        private string Execute(string soapAction, string message, string responseTag)
        {
            return Execute(soapAction, message, responseTag, "xmlns:\"http://www.abrasf.org.br\"");
        }

        protected override string TratarRetorno(XDocument xmlDocument, string[] responseTag)
        {
            var element = xmlDocument.ElementAnyNs("Fault");
            if (element != null)
            {
                var exMessage = $"{element.ElementAnyNs("faultcode").GetValue<string>()} - {element.ElementAnyNs("faultstring").GetValue<string>()}";
                throw new ACBrDFeCommunicationException(exMessage);
            }

            var reader = xmlDocument.ElementAnyNs(responseTag[0]).CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }

        #endregion Methods
    }
}