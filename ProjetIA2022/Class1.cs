using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace ProjetIA2022
{
    public class Node2 : GenericNode
    {
        public int x;
        public int y;

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public override bool IsEqual(GenericNode N2)
        {
            Node2 N2bis = (Node2)N2;

            return (x == N2bis.x) && (y == N2bis.y);
        }
        public override double GetArcCost(GenericNode N2)
        {
            // Ici, N2 ne peut être qu'1 des 8 voisins, inutile de le vérifier
            Node2 N2bis = (Node2)N2;
            double dist = Math.Sqrt((N2bis.x - x) * (N2bis.x - x) + (N2bis.y - y) * (N2bis.y - y));
            if (Form1.matrice[x, y] == -1)
                // On triple le coût car on est dans un marécage
                dist = dist * 3;
            return dist;
        }
        public override bool EndState()
        {
            return (x == Form1.xfinal) && (y == Form1.yfinal);
        }
        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if ((x + dx >= 0) && (x + dx < Form1.nbcolonnes)
                            && (y + dy >= 0) && (y + dy < Form1.nblignes) && ((dx != 0) || (dy != 0)))
                        if (Form1.matrice[x + dx, y + dy] > -2)
                        {
                            Node2 newnode2 = new Node2();
                            newnode2.x = x + dx;
                            newnode2.y = y + dy;
                            lsucc.Add(newnode2);
                        }
                }

            }
            return lsucc;
        }

        static double ShortestRoadWithoutPerturbation(int xcurrent, int ycurrent, int xfinal, int yfinal)
        {
            double dist = 0;
            while (xcurrent != xfinal || ycurrent != yfinal)
            {
                //Déplacement selon x
                if (xcurrent - xfinal < 0) xcurrent += 1; // si le x courant est positionné à gauche du x final 
                else if (xcurrent - xfinal > 0) xcurrent -= 1; //sinon le point courrant est à droite du point final
                if (ycurrent - yfinal < 0) ycurrent += 1; //si le y courrant est au dessous du y final  
                else if (ycurrent - yfinal > 0) ycurrent -= 1; //sinon le y courran  est au dessus du y final
                dist += Math.Pow(2, 2);
            }
            while (xcurrent != xfinal && ycurrent != yfinal)
            {
                if (xcurrent != xfinal)
                {
                    if (xcurrent - xfinal < 0) xcurrent += 1; // si le x courant est positionné à gauche du x final 
                    else if (xcurrent - xfinal > 0) xcurrent -= 1; //sinon le point courrant est à droite du point final
                }
                if (ycurrent != yfinal)
                {
                    if (ycurrent - yfinal < 0) ycurrent += 1; //si le y courrant est au dessous du y final  
                    else if (ycurrent - yfinal > 0) ycurrent -= 1; //sinon le y courran  est au dessus du y final
                }
                dist += 1;
            }
            return dist;
        }
        static double ShortestRoadWithPerturbation(int xcurrent, int ycurrent, int xfinal, int yfinal)//fonction a revoir car imcomplète 
        {
            int diag = 0;
            int diagM = 0;
            int lign = 0;
            int lignM = 0;
            double dist = 0;
            while (xcurrent != xfinal && ycurrent != yfinal)
            {
                diag += 1;
                //Déplacement selon x
                if (xcurrent - xfinal < 0) xcurrent += 1; // si le x courant est positionné à gauche du x final 
                else if (xcurrent - xfinal > 0) xcurrent -= 1; //sinon le point courrant est à droite du point final
                //Déplacement selon y
                if (ycurrent - yfinal < 0) ycurrent += 1; //si le y courrant est au dessous du y final  
                else if (ycurrent - yfinal > 0) ycurrent -= 1; //sinon le y courran  est au dessus du y final
                //Valeur distance si perturbation ou non
                if (Form1.matrice[xcurrent, ycurrent] == -1)
                {
                    dist += Math.Pow(2, 2) * 3;
                    diagM += 1; //Avec Perturbation
                }
                else dist += Math.Pow(2, 2);


            }
            while (xcurrent != xfinal || ycurrent != yfinal)
            {
                lign += 1;
                if (xcurrent != xfinal)
                {
                    if (xcurrent < xfinal ) xcurrent += 1; // si le x courant est positionné à gauche du x final 
                    else if (xcurrent > xfinal) xcurrent -= 1; //sinon le point courrant est à droite du point final
                }
                if (ycurrent != yfinal)
                {
                    if (ycurrent < yfinal) ycurrent += 1; //si le y courrant est au dessous du y final  
                    else if (ycurrent > yfinal) ycurrent -= 1; //sinon le y courran  est au dessus du y final
                }
                if (Form1.matrice[xcurrent, ycurrent] == -1)
                {
                    dist += 3; //Avec Perturbation
                    lignM += 1;
                }
                else dist += 1;
            }
            Console.WriteLine(x + "," + y + ", H : " + dist + "\n - diag : " + diag + " dont M : " + diagM + "\n _ lign : " + lign + " dont M : " + lignM);
            return dist;
        }
        static double Manhattan(int xcurrent, int ycurrent, int xfinal, int yfinal)
        {
            int distX(int xc, int yc, int xf, int yf)
            {
                int dist = 0;
                while (xc != xf)
                {
                    if (xc < xf) xc += 1; // si le x courant est positionné à gauche du x final 
                    else if (xc > xf) xc -= 1; //sinon le point courrant est à droite du point final
                    if (Form1.matrice[xc, yc] == -1)
                    {
                        dist += 3; //Avec Perturbation
                    }
                    else dist += 1;
                }
                return dist;
            }
            int distY(int xc, int yc, int xf, int yf){
                int dist = 0;
                while (yc != yf)
                {
                    if (yc < yf) yc += 1; //si le y courrant est au dessous du y final  
                    else if (yc > yf) yc -= 1; //sinon le y courran  est au dessus du y final
                    if (Form1.matrice[xc, yc] == -1)
                    {
                        dist += 3; //Avec Perturbation
                    }
                    else dist += 1;
                }
                //Console.WriteLine(x + "," + y + ", H : " + dist );
                return dist;
            }
            int dist1 = distX(xcurrent, ycurrent, xfinal, yfinal) + distY(xcurrent, ycurrent, xfinal, yfinal);
            int dist2 = distY(xcurrent, ycurrent, xfinal, yfinal) + distX(xcurrent, ycurrent, xfinal, yfinal);
            return Math.Min(dist1,dist2);
        }
        
        public override void calculCoutTotal(Func<int,int,int,int,double> EmpiricFunction)
        {
            HCost = CalculeHCost(Form1.environment,EmpiricFunction);
            TotalCost = GCost + HCost;
        }
        public override double CalculeHCost(int envt,Func<int,int,int,int,double> EmpiricFunction)
        {
            if (envt == 1) return HCostEvnt1(EmpiricFunction);
            else if (envt == 2) return HCostEvnt2(EmpiricFunction);
            else if (envt == 3) return HCostEvnt3(EmpiricFunction);
            return (0);
        }
        private double HCostEvnt1(Func<int,int,int,int,double> EmpiricFunction)
        {
            //première version primitive retournant un H qui correspond au temps minimum necessaire pour aller 
            //jusqu'à l'objectif avec un chemin choisi à vol d'oiseau sans tennir compte des obstacles ou ralentisseurs
            int xcurrent = x;
            int ycurrent = y;
            int xfinal = Form1.xfinal;
            int yfinal = Form1.yfinal;
            double dist = EmpiricFunction(xcurrent, ycurrent, xfinal, yfinal);
            return dist;
        }
        private double HCostEvnt2(Func<int,int,int,int,double> EmpiricFunction)
        {
            int xcurrent = x;
            int ycurrent = y;
            int xfinal = Form1.xfinal;
            int yfinal = Form1.yfinal;
            int xintermediaire = 10;
            int yintermedaire = 8;
            double HCostInterFinal = EmpiricFunction(xintermediaire, yintermedaire, xfinal, yfinal);
            if ((xcurrent < xintermediaire && xfinal < xintermediaire) || (xcurrent > xintermediaire && xfinal > xintermediaire))
            {
                return EmpiricFunction(xcurrent, ycurrent, xfinal, yfinal);
            }
            else
            {
                if (xcurrent < xintermediaire)
                {
                    return EmpiricFunction(xcurrent, ycurrent, xintermediaire, yintermedaire) + HCostInterFinal;
                }
                else
                {
                    return EmpiricFunction(xcurrent, ycurrent, xfinal, yfinal);
                }
            }
            return -1;
        }
        private double HCostEvnt3(Func<int,int,int,int,double> EmpiricFunction)
        {
            int xcurrent = x;
            int ycurrent = y;
            int xfinal = Form1.xfinal;
            int yfinal = Form1.yfinal;
            int xinter1 = 10;
            int yinter1 = 0;
            int xinter2 = 2;
            int yinter2 = 6;

            int[,] positionEnclos = new int[Form1.nblignes,Form1.nbcolonnes];

            positionEnclos[3,6] = 1;
            positionEnclos[3,7] = 1;

            positionEnclos[4,5] = 1;
            positionEnclos[4,6] = 1;
            positionEnclos[4,7] = 1;
            positionEnclos[4,8] = 1;

            positionEnclos[5,4] = 1;
            positionEnclos[5,5] = 1;
            positionEnclos[5,6] = 1;
            positionEnclos[5,7] = 1;
            positionEnclos[5,8] = 1;
            positionEnclos[5,9] = 1;

            positionEnclos[6,4] = 1;
            positionEnclos[6,5] = 1;
            positionEnclos[6,6] = 1;
            positionEnclos[6,7] = 1;
            positionEnclos[6,8] = 1;
            positionEnclos[6,9] = 1;

            positionEnclos[8,6] = 1;
            positionEnclos[8,7] = 1;

            if (xcurrent > xinter1 && xfinal > xinter1) //point courrant et final à doite de la barrière
            {
                return EmpiricFunction(xcurrent, ycurrent, xfinal, yfinal);
            }
            else if(xcurrent < xinter1 && xfinal < xinter1){ //point courrant et final à gauche de la barrière
                    if(positionEnclos[xcurrent,ycurrent]==1 && positionEnclo[xfinal,yfinal]==1){ //point courrant et final dans l'enclos
                        return EmpiricFunction(xcurrent,ycurrent,xfinal,yfinal);
                    }
                    else if(positionEnclos(xfinal,yfinal)==1){ //point final dans l'enclos, courrant hors
                        int distInter = EmpiricFunction(xcurrent,ycurrent,xinter2,yinter2);
                        int distEnclos = EmpiricFunction(xinter2,yinter2,xfinal,yfinal);
                        return distInter + distEnclos;
                    }
                    else if(positionEnclos[xcurrent,ycurrent]==1){ //point courrant dans l'enclos , final hors
                        int distOutEnclos = EmpiricFunction(xcurrent,ycurrent,xinter2,yinter2);
                        int distToFinal = EmpiricFunction(xinter2,yinter2,xfinal,yfinal);
                        return distOutEnclos + distToFinal;
                    }
                    else; return EmpiricFunction(xcurrent,ycurrent,xfinal,yfinal); //point courrant et final hors de l'enclos
            }
            else if (xcurrent > xinter1 && xfinal < xinter1){ //point courrant à droite de la barrière, point fianl à gauche
                int distInter = EmpiricFunction(xcurrent,ycurrent,xinter1,yinter1);
                if(positionEnclos(xfinal,yfinal)==1){ //point final dans l'enclos
                        int distToEnclos = EmpiricFunction(xinter1,yinter1,xinter2,yinter2);
                        int distInEnclos = EmpiricFunction(xinter2,yinter2,xfinal,yfinal);
                        return distInter + distToEnclos + distInEnclos;
                    }
                else{ // point final hors de l'enclos
                    int distFinal = EmpiricFunction(xinter1,yinter1,xfinal,yfinal);
                    return distInter + distFinal;
                }
            }
            else if(xcurrent < xinter1 && xfinal > xinter1){ //point courrant à gauche de la barrière, point final à droite
                if(positionEnclos[xcurrent,ycurrent]==1){ //point courrant dans l'enclos
                    int distOutEnclos = EmpiricFunction(xcurrent,ycurrent,xinter2,yinter2);
                    int distToInter = EmpiricFunction(xinter2,yinter2,xinter1,yinter1);
                    int distToFinal = EmpiricFunction(xinter1,yinter1,xfinal,yfinal);
                    return distOutEnclos + distToInter + distToFinal;
                }
                else{ //point courrant hors de l'enclos
                    int distToInter = EmpiricFunction(xcurrent,ycurrent,xinter1,yinter1);
                    int distToFinal = EmpiricFunction(xinter1,yinter1,xfinal,yfinal);
                    return distToInter + distToFinal;
                }
            }
            return -1;
        }

        public override string ToString()
        {
            return Convert.ToString(x) + "," + Convert.ToString(y) + ", H : " + Convert.ToString(HCost) + ", F : " + Convert.ToString((int)(HCost+GCost));
        }
    }
}

