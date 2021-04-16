﻿// ***********************************************************************
// Assembly         : ACBr.Net.NFSe
// Author           : Rafael Dias
// Created          : 12-26-2017
//
// Last Modified By : Rafael Dias
// Last Modified On : 23-01-2020
// ***********************************************************************
// <copyright file="SimplISSServiceClient.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
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
using System.Xml.Linq;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.NFSe.Providers
{
    internal sealed class SimplISSServiceClient : NFSeSOAP11ServiceClient, IServiceClient
    {
        #region Constructors

        public SimplISSServiceClient(ProviderSimplISS provider, TipoUrl tipoUrl) : base(provider, tipoUrl)
        {
        }

        #endregion Constructors

        #region Methods

        public string Enviar(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<sis:RecepcionarLoteRps xmlns=\"http://www.sistema.com.br/Sistema.Ws.Nfse\">");
            message.Append(msg.Replace("EnviarLoteRpsEnvio", "sis:EnviarLoteRpsEnvio"));
            message.Append("<sis:pParam>");
            message.Append($"<P1 xmlns=\"http://www.sistema.com.br/Sistema.Ws.Nfse.Cn\">{Provider.Configuracoes.WebServices.Usuario}</P1>");
            message.Append($"<P2 xmlns=\"http://www.sistema.com.br/Sistema.Ws.Nfse.Cn\">{Provider.Configuracoes.WebServices.Senha}</P2>");
            message.Append("</sis:pParam>");
            message.Append("</sis:RecepcionarLoteRps>");

            return Execute("http://www.sistema.com.br/Sistema.Ws.Nfse/INfseService/RecepcionarLoteRps", message.ToString(), "RecepcionarLoteRpsResponse");
        }

        public string EnviarSincrono(string cabec, string msg)
        {
            throw new NotImplementedException();
        }

        public string ConsultarSituacao(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<e:ConsultarSituacaoLoteRPS.Execute>");
            message.Append("<e:Nfsecabecmsg>");
            message.AppendCData(cabec);
            message.Append("</e:Nfsecabecmsg>");
            message.Append("<e:Nfsedadosmsg>");
            message.AppendCData(msg);
            message.Append("</e:Nfsedadosmsg>");
            message.Append("</e:ConsultarSituacaoLoteRPS.Execute>");

            return Execute("http://www.e-nfs.com.braction/ACONSULTARSITUACAOLOTERPS.Execute", message.ToString(), "ConsultarSituacaoLoteRPS.ExecuteResponse");
        }

        public string ConsultarLoteRps(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<e:ConsultarLoteRps.Execute>");
            message.Append("<e:Nfsecabecmsg>");
            message.AppendCData(cabec);
            message.Append("</e:Nfsecabecmsg>");
            message.Append("<e:Nfsedadosmsg>");
            message.AppendCData(msg);
            message.Append("</e:Nfsedadosmsg>");
            message.Append("</e:ConsultarLoteRps.Execute>");

            return Execute("http://www.e-nfs.com.braction/ACONSULTARLOTERPS.Execute", message.ToString(), "ConsultarLoteRps.ExecuteResponse");
        }

        public string ConsultarSequencialRps(string cabec, string msg)
        {
            throw new NotImplementedException();
        }

        public string ConsultarNFSeRps(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<e:ConsultarNfsePorRps.Execute>");
            message.Append("<e:Nfsecabecmsg>");
            message.AppendCData(cabec);
            message.Append("</e:Nfsecabecmsg>");
            message.Append("<e:Nfsedadosmsg>");
            message.AppendCData(msg);
            message.Append("</e:Nfsedadosmsg>");
            message.Append("</e:ConsultarNfsePorRps.Execute>");

            return Execute("http://www.e-nfs.com.braction/ACONSULTARNFSEPORRPS.Execute", message.ToString(), "ConsultarNfsePorRps.ExecuteResponse");
        }

        public string ConsultarNFSe(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<e:ConsultarNfse.Execute>");
            message.Append("<e:Nfsecabecmsg>");
            message.AppendCData(cabec);
            message.Append("</e:Nfsecabecmsg>");
            message.Append("<e:Nfsedadosmsg>");
            message.AppendCData(msg);
            message.Append("</e:Nfsedadosmsg>");
            message.Append("</e:ConsultarNfse.Execute>");

            return Execute("http://www.e-nfs.com.braction/ACONSULTARNFSE.Execute", message.ToString(), "ConsultarNfse.ExecuteResponse");
        }

        public string CancelarNFSe(string cabec, string msg)
        {
            var message = new StringBuilder();
            message.Append("<e:CancelarNfse.Execute>");
            message.Append("<e:Nfsecabecmsg>");
            message.AppendCData(cabec);
            message.Append("</e:Nfsecabecmsg>");
            message.Append("<e:Nfsedadosmsg>");
            message.AppendCData(msg);
            message.Append("</e:Nfsedadosmsg>");
            message.Append("</e:CancelarNfse.Execute>");

            return Execute("http://www.e-nfs.com.braction/ACANCELARNFSE.Execute", message.ToString(), "CancelarNfse.ExecuteResponse");
        }

        public string CancelarNFSeLote(string cabec, string msg)
        {
            throw new NotImplementedException();
        }

        public string SubstituirNFSe(string cabec, string msg)
        {
            throw new NotImplementedException();
        }

        private string Execute(string soapAction, string message, string responseTag)
        {
            return Execute(soapAction, message, "", responseTag, "xmlns:sis=\"http://www.sistema.com.br/Sistema.Ws.Nfse\"");
        }

        protected override string TratarRetorno(XDocument xmlDocument, string[] responseTag)
        {
            var element = xmlDocument.ElementAnyNs("Fault");
            if (element == null) return xmlDocument.ElementAnyNs(responseTag[0]).Value;

            var exMessage = $"{element.ElementAnyNs("faultcode").GetValue<string>()} - {element.ElementAnyNs("faultstring").GetValue<string>()}";
            throw new ACBrDFeCommunicationException(exMessage);
        }

        #endregion Methods
    }
}