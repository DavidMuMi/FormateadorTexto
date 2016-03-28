using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text.RegularExpressions;
namespace FormateadorDeTexto
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ArrayList Silabas = new ArrayList();
        ArrayList Diptongos = new ArrayList();
        ArrayList Triptongos = new ArrayList();
        ArrayList Vocales = new ArrayList();
        ArrayList Doble = new ArrayList();
        ArrayList RL = new ArrayList();
        ArrayList Prefijos = new ArrayList();
        ArrayList Puntuacion = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            // Read the file and display it line by line.
            //var dataFile = Server.MapPath("~/sample.txt");
            //System.IO.StreamReader file = new System.IO.StreamReader(dataFile);

            Diptongos.Add("ai");
            Diptongos.Add("au");
            Diptongos.Add("ei");
            Diptongos.Add("eu");
            Diptongos.Add("oi");
            Diptongos.Add("ou");
            Diptongos.Add("ia");
            Diptongos.Add("ie");
            Diptongos.Add("io");
            Diptongos.Add("iu");
            Diptongos.Add("ua");
            Diptongos.Add("ue");
            Diptongos.Add("ui");
            Diptongos.Add("uo");

            Triptongos.Add("iau");
            Triptongos.Add("iai");
            Triptongos.Add("uai");
            Triptongos.Add("uau");
            Triptongos.Add("ieu");
            Triptongos.Add("iei");
            Triptongos.Add("uei");
            Triptongos.Add("ueu");
            Triptongos.Add("iou");
            Triptongos.Add("ioi");
            Triptongos.Add("uoi");
            Triptongos.Add("uou");
            Triptongos.Add("uie");

            Vocales.Add('a');
            Vocales.Add('e');
            Vocales.Add('i');
            Vocales.Add('o');
            Vocales.Add('u');

            Doble.Add("ll");
            Doble.Add("rr");
            Doble.Add("ch");

            RL.Add('r');
            RL.Add('l');
            /*
            Prefijos.Add("des");
            Prefijos.Add("pos");
            Prefijos.Add("post");
            Prefijos.Add("in");
            Prefijos.Add("re");
            Prefijos.Add("bi");
            Prefijos.Add("pre");
            Prefijos.Add("sub");
            Prefijos.Add("pro");
            Prefijos.Add("dis");
            Prefijos.Add("geo");
            Prefijos.Add("abs");
            Prefijos.Add("bis");
            Prefijos.Add("im");
            Prefijos.Add("tri");
            Prefijos.Add("sin");
            Prefijos.Add("trans");
            */
            Puntuacion.Add(';');
            Puntuacion.Add(',');
            Puntuacion.Add('.');
            Puntuacion.Add(':');
            Puntuacion.Add('?');
            Puntuacion.Add('¿');
            Puntuacion.Add('!');
            Puntuacion.Add('¡');
            Puntuacion.Add('"');
            Puntuacion.Add('(');
            Puntuacion.Add(')');

        }

        protected void Formatear(object sender, EventArgs e)
        {
            //String Cadena = "@"; 
            String Cadena = "";           
            String CadenaParseada = "";
            MatchCollection matches;
            Regex regex;
            Cadena += TextoAFormatear.Text;
            Cadena += " ";
            Cadena = Cadena.Replace("\n", "");
            int cols = int.Parse(Cols.Text);
            TextoFormateado.Columns = cols + 5;
            TextoFormateado.Visible = true;
            CadenaParseada = parseo(Cadena, CadenaParseada, cols , 0);
            if (CadenaParseada == null)
            {
                TextoFormateado.Text = "La cadena no se puede dividir con esas columnas";
                TextoFormateado.Columns = 30;
                return;
            }

            regex = new Regex(@"(\n)");
            matches = regex.Matches(CadenaParseada);
            TextoFormateado.Rows = matches.Count + 1;
            CadenaParseada = CadenaParseada.Replace("\n\n","\n");
            CadenaParseada = CadenaParseada.Replace("\n-\n", "\n");
            TextoFormateado.Text = CadenaParseada;

        }
        protected string parseo(String CadenaOrigen, String CadenaParseada, int cols, int tamanioAct)
        {
            if (CadenaOrigen.Length>0 && CadenaOrigen[0] == 32 && tamanioAct==0)
            {
                CadenaOrigen = CadenaOrigen.Remove(0, 1);
            }
            //A la palabra le sobra espacio en la fila
            if (((CadenaOrigen.IndexOf(" ") + tamanioAct) < (cols - 1)) && CadenaOrigen.Length > 0)
            {
                /*
                if (CadenaOrigen[0] == '@')
                {
                    CadenaOrigen = CadenaOrigen.Remove(0, 1);
                }
                */
                if (CadenaOrigen.IndexOf(" ") == -1)
                {
                    CadenaParseada += CadenaOrigen.Substring(0, CadenaOrigen.Length);
                    tamanioAct += CadenaOrigen.Length;
                    CadenaOrigen = CadenaOrigen.Remove(0, CadenaOrigen.Length);
                }
                else
                {
                    CadenaParseada += CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" ") + 1);
                    tamanioAct += CadenaOrigen.IndexOf(" ") + 1;
                    CadenaOrigen = CadenaOrigen.Remove(0, CadenaOrigen.IndexOf(" ") + 1);

                }
                return parseo(CadenaOrigen, CadenaParseada, cols, tamanioAct);

            }
            //A la palabra le falta una columna para entrar justo
            else if ((CadenaOrigen.IndexOf(" ") + tamanioAct) == (cols - 1) && CadenaOrigen.Length > 0)
            {
                /*
                if (CadenaOrigen[0] == '@')
                {
                    CadenaOrigen = CadenaOrigen.Remove(0, 1);
                }*/
                if (CadenaOrigen.IndexOf(" ") == -1)
                {
                    CadenaParseada += CadenaOrigen.Substring(0, CadenaOrigen.Length);
                    tamanioAct += CadenaOrigen.Length;
                    CadenaOrigen = CadenaOrigen.Remove(0, CadenaOrigen.Length);
                }
                else
                {
                    CadenaParseada += CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" "));
                    CadenaParseada += '\n';
                    tamanioAct = 0;
                    CadenaOrigen = CadenaOrigen.Remove(0, CadenaOrigen.IndexOf(" ") + 1);
                }
                return parseo(CadenaOrigen, CadenaParseada, cols, tamanioAct);
            }
            else if ((CadenaOrigen.IndexOf(" ") + tamanioAct == (cols) && CadenaOrigen.Length > 0))
            {   /*
                if (CadenaOrigen[0] == '@')
                {
                 CadenaOrigen=  CadenaOrigen.Remove(0, 1);
                }*/

                CadenaParseada += CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" "));
                tamanioAct += CadenaOrigen.IndexOf(" ");
                CadenaOrigen = CadenaOrigen.Remove(0, CadenaOrigen.IndexOf(" ") + 1);
                CadenaParseada += '\n';
                tamanioAct = 0;
                return parseo(CadenaOrigen, CadenaParseada, cols, tamanioAct);
            }

            //Caso dividir la palabra
            else if ((CadenaOrigen.IndexOf(" ") + tamanioAct) > (cols - 1) && CadenaOrigen.Length > 0)
            {
                String silaba;
                /*
                if (CadenaOrigen[0] == '@')
                {
                    silaba = siguiente_silaba(CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" ") + 1));
                    CadenaOrigen = CadenaOrigen.Remove(0, 1);
                }
                else if (CadenaParseada[CadenaParseada.Length - 1] == 32)
                {
                    silaba = siguiente_silaba("@"+CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" ") + 1));
                }else if (CadenaParseada[CadenaParseada.Length - 1] == '\n' && !(CadenaParseada[CadenaParseada.Length - 2] == '-'))
                {
                    silaba = siguiente_silaba("@" + CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" ") + 1));
                }
                else
                */
                    silaba = siguiente_silaba(CadenaOrigen.Substring(0, CadenaOrigen.IndexOf(" ")+1));

                /*
                if (silaba.Length >= cols)
                    //if(!(CadenaOrigen[silaba.Length]==32))
                        return null;
                */
                if (silaba.Length == 0)
                {
                    CadenaOrigen = CadenaOrigen.Remove(0, 1);
                    CadenaParseada += "\n";
                    tamanioAct = 0;
                }
                else if(silaba.Length + tamanioAct > cols)
                {
                    if (CadenaParseada.Length > 0 && CadenaParseada[CadenaParseada.Length - 1] != 32)
                    {
                        CadenaParseada += "-";

                    }
                    CadenaParseada += "\n";
                    tamanioAct = 0;
                   

                    CadenaParseada += CadenaOrigen.Substring(0, silaba.Length);
                    tamanioAct += CadenaOrigen.Substring(0, silaba.Length).Length;
                    CadenaOrigen = CadenaOrigen.Remove(0, silaba.Length);
                    
                }/*
                else if (silaba.Length + tamanioAct > cols)
                {
                    if (CadenaParseada.Length != 0  && tamanioAct>1 && CadenaParseada[CadenaParseada.Length - 1] != 32)
                    {
                        CadenaParseada += "-";
                    }
                    CadenaParseada += "\n";
                    tamanioAct = 0;
                    CadenaParseada += CadenaOrigen.Substring(0, silaba.Length);
                    CadenaOrigen = CadenaOrigen.Remove(0, silaba.Length);
                    if (CadenaParseada.Length!=0 && (CadenaOrigen[0] != 32))
                    {
                        CadenaParseada += "-";
                    }

                    CadenaParseada += "\n";
                    tamanioAct = 0;

                }*//*
                else if (silaba.Length + 1 + tamanioAct == cols)
                {
                    CadenaParseada += CadenaOrigen.Substring(0, silaba.Length);
                    CadenaParseada += "-";
                    CadenaParseada += "\n";
                    tamanioAct = 0;
                    CadenaOrigen = CadenaOrigen.Remove(0, silaba.Length);
                }*/
                else
                {
                    CadenaParseada += CadenaOrigen.Substring(0, silaba.Length);
                    CadenaOrigen = CadenaOrigen.Remove(0, silaba.Length);
                    tamanioAct += silaba.Length;
                }
                return parseo(CadenaOrigen, CadenaParseada, cols, tamanioAct);

            }

            return CadenaParseada;
        }
        protected string siguiente_silaba (String palabra_rcv){
            String palabra = palabra_rcv.ToLower();
            String silaba="";
            int letras = 1;
            int i;
            int vocales=0;
            palabra = palabra.Replace("á", "a");
            palabra = palabra.Replace("é", "e");
            palabra = palabra.Replace("í", "i");
            palabra = palabra.Replace("ó", "o");
            palabra = palabra.Replace("u", "u");

            if (Regex.IsMatch(palabra.Substring(0, palabra.IndexOf(" ") + 1), @"\d"))
            {
                return palabra.Substring(0,palabra.Length-1);
            }

            //Compruebo si es una palabra nueva para busacar si tiene prefijo
            /*
            if (palabra[0] == '@')
            {
                palabra=palabra.Remove(0, 1);
                if (palabra.Length > 4 && Prefijos.Contains(palabra.Substring(0, 5)))
                {
                    letras += 4;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                if (palabra.Length > 3 && Prefijos.Contains( palabra.Substring(0, 4)))
                {
                    letras += 3;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                else if (palabra.Length > 2 &&  Prefijos.Contains(palabra.Substring(0, 3)))
                {
                    letras += 2;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                else if (Prefijos.Contains(palabra.Substring(0, 2)))
                {
                    letras += 1;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
            }
            */
            for (i = 0; i < palabra.Length; i++)
            {
                if (Vocales.Contains(palabra[i]))
                {
                    vocales++;
                }
            }
            if (vocales == 0)
            {
                return palabra.Substring(0, palabra.Length - 1);
            }

            if (Puntuacion.Contains(palabra[0]))
            {
                silaba += palabra.Substring(0, 1);
            }
            
            //Localizo la primera vocal de la silaba
            for (i = 0; !Vocales.Contains(palabra[i]); i++) ;
            letras += i;
            //miro si la palabra termina
            if (palabra[letras] == 32)
            {
                silaba += palabra.Substring(0, letras);
                palabra.Remove(0, letras);
                return silaba;
            }else if (palabra[letras + 1] == 32 )
            {
                letras += 1;
                silaba += palabra.Substring(0, letras);
                palabra.Remove(0, letras);
                return silaba;
            }
            else if (palabra[letras + 2] == 32 && !Vocales.Contains(palabra[letras + 1]))
            {
                letras += 2;
                silaba += palabra.Substring(0, letras);
                palabra.Remove(0, letras);
                return silaba;
            }

            //las vocales forman hiato
            if (Vocales.Contains(palabra[letras]) && !Diptongos.Contains(palabra.Substring(letras-1, 2)))
                {
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }

                //La silaba contiene triptongo
                if (Triptongos.Contains(palabra.Substring(letras-1, 3)))
                {
                    letras += 2;
                if (palabra[letras] == 32)
                {
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                else if (palabra[letras + 1] == 32)
                {
                    letras += 1;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                else if (palabra[letras + 2] == 32 && !Vocales.Contains(palabra[letras + 1]))
                {
                    letras += 2;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
            }
            //La silaba contiene un diptongo
            else if (Diptongos.Contains(palabra.Substring(letras-1, 2))){
                    letras += 1;
                if (palabra[letras] == 32)
                {
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                else if (palabra[letras + 1] == 32)
                {
                    letras += 1;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                else if (palabra[letras + 2] == 32 && !Vocales.Contains(palabra[letras + 1]))
                {
                    letras += 2;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
            }

                //Una consonante entre dos vocales, se agrupan con la vocal de la derecha, rr o ll cuentan como una consonante

                if (Vocales.Contains(palabra[letras+1]) || Doble.Contains(palabra.Substring(letras,2)))
                {
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }

                //Dos consonantes entre vocales se separan y cada consonante se queda con una vocal, a no ser que la segunda sea r o l, en cuyo caso las dos consonantes se agrupan con la segunda vocal
                else if(Vocales.Contains(palabra[letras+2])){
                    if (RL.Contains(palabra[letras + 1]))
                    {
                        silaba += palabra.Substring(0, letras);
                        palabra.Remove(0, letras);
                        return silaba;
                    }
                    else
                    {
                        letras += 1;
                        silaba += palabra.Substring(0, letras);
                        palabra.Remove(0, letras);
                        return silaba;
                    }
                }
                //tres consonantes entre vocales, las primeras dos se unen con la primera voval y la tercera se una a la segunda vocal, a no ser que la tercera sea r o l, en ese caso sólo una consonante va con la primera vocal
                else if (Vocales.Contains(palabra[letras + 3]))
                {
                if (Doble.Contains(palabra.Substring(letras+1,2)))
                {
                    letras += 1;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                }
                    else if (RL.Contains(palabra[letras + 2]))
                    {
                        letras += 1;
                        silaba += palabra.Substring(0, letras);
                        palabra.Remove(0, letras);
                        return silaba;
                    }
                    else
                    {
                        letras += 2;
                        silaba += palabra.Substring(0, letras);
                        palabra.Remove(0, letras);
                        return silaba;
                    }
                }
                //Cuatro consonantes entre vocales las dos primeras a la primera vocal.
                else
                {
                    letras += 2;
                    silaba += palabra.Substring(0, letras);
                    palabra.Remove(0, letras);
                    return silaba;
                } 
            
        }

    }
}