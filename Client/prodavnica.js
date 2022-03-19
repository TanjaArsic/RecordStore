import { Vinyl } from "./vinyl.js";
// // import{ Prodavac } from "./prodavac.cs"

export class Prodavnica{
    constructor(id,naziv,adresa,mail,brVinyl){
        this.id=id;
        this.naziv=naziv;
        this.adresa=adresa;
        this.mail=mail;
        this.brVinyl=brVinyl;
        this.kont=null;    
        this.ploce=[];
    }

    dodajPlocu(pl){
        let p=this.kont.querySelector(".PRODAVNICA");
        p=p.innerHTML;

        let q=this.kont.querySelector(".nazivploce");
        q=q.innerHTML;

        let q1=this.kont.querySelector(".izvodjacploce");
        q1=q1.innerHTML;

        let q2=this.kont.querySelector(".izdavackakucaploce");
        q2=q2.innerHTML;
        let q3=this.kont.querySelector(".godstampanjaploce");
        q3=q3.innerHTML;
        let q4=this.kont.querySelector(".pesmeploce");
        q4=q4.innerHTML;
        let q5=this.kont.querySelector(".cenaploce");
        q5=q5.innerHTML;
        
        let m=this.kont.querySelector(".selektZanra");
        let  zanr=m.querySelector('input[name="selektZanra"]').value;

        console.log(m);
        console.log(zanr);
        fetch("https://localhost:5001/SpojProdavnicaPloca/DodajPlocu/" + p + "/" + q + "/" + q1 + "/" + q3 + "/" + zanr+"/" + q4+"/" + q2+"/" + q5,
        {
            method: "POST",
      
        }).then(p => {
            if (p.ok) {
               console.log("Uspesno uneta ploca!");   
              
              
          }
             
            
                       
              });
        
    }

    crtaj(host, gost){  //<!---crtajprodavnicu--->

        this.kont=document.createElement("div");
        this.kont.className="GlavniKontejner";
        host.appendChild(this.kont);

        let PRODAVNICA =document.createElement("label");
        PRODAVNICA.className="PRODAVNICA";
        PRODAVNICA.innerHTML=gost;
        this.kont.appendChild(PRODAVNICA);

        let VelikiDiv=document.createElement("div");
        VelikiDiv.className="VELIKIDIV";
        this.kont.appendChild(VelikiDiv);

        let kontForma=document.createElement("div");
        kontForma.className="Forma";
        VelikiDiv.appendChild(kontForma);

        let PlocaTabela=document.createElement("div");
        PlocaTabela.className="PlocaTabela";
        VelikiDiv.appendChild(PlocaTabela);

        let tabelaRadnici=document.createElement("div");
        tabelaRadnici.className="TabelaRadnici";
        this.kont.appendChild(tabelaRadnici);
        
        // const btn=document.createElement("button");
        // btn.addEventListener=(ev)

    

         this.crtajFormu(kontForma);
        
    }

    crtajFormu(kontForma){


        let nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Naziv: ");
        let input=document.createElement("input");
        input.className="nazivploce";
        nekidiv.appendChild(input);

        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Izvodjac: ");
        input=document.createElement("input");
        input.className="izvodjacploce";
        nekidiv.appendChild(input);

        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Izdavacka kuca: ");
        input=document.createElement("input");
        input.className="izdavackakucaploce";
        nekidiv.appendChild(input);

        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Godina stampanja: ");
        input=document.createElement("input");
        input.className="godstampanjaploce";
        nekidiv.appendChild(input);

        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Pesme: ");
       //input=document.createElement("input");
       input=document.createElement("textarea");
        input.className="pesmeploce";
        nekidiv.appendChild(input);
        
        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Zanr: ");
        let sel=document.createElement("select");
        sel.className="selektZanra";
        nekidiv.appendChild(sel);

        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Cena: ");
        input=document.createElement("input");
        input.className="cenaploce";
        nekidiv.appendChild(input);

        nekidiv=this.crtajDiv(kontForma);
        let btn=document.createElement("button");
        btn.innerHTML="OK";
        nekidiv.appendChild(btn);
        btn.onclick=(ev)=>this.dodajPlocu();

        nekidiv=this.crtajDiv(kontForma);
        let btn1=document.createElement("button");
        btn1.innerHTML="Prikazi ploce";
        nekidiv.appendChild(btn1);
        btn1.onclick=(ev)=>this.prikaziploce();

        this.crtajSelect(sel);
    }
    prikaziploce()
    {
        let m=this.kont.querySelector(".PlocaTabela");
        let p=this.kont.querySelector(".PRODAVNICA");
        p=p.innerHTML;
        let qlist=[];
        let  q;
        let i=0;
        console.log(this.kont);
        fetch("https://localhost:5001/Prodavnica/Prodavnica+Ploce/"+ p,
        {
            method:"GET",

        }).then(p=>{
            if(p.ok)
            {
                
                    p.json().then(ploce=>{
                        console.log(ploce);
                        ploce.forEach(i=>{
                            q=new Vinyl(i.ploca.id,i.ploca.ime,i.ploca.zanr,i.ploca.pesme,i.ploca.godinaStampanja,i.cena,i.kolicina,i.izvodjac);
                           q.crtajVinyl(m);
                         qlist.push(q);
         });
                    })
                }
            })
            let e=new Prodavnica();
            let o=this.kont.querySelector(".Forma");
            console.log(qlist);
            let n=document.querySelectorAll(".PlocaTabela");
            n.forEach(function(a){
            a.addEventListener("click", function(){
                
               e.popuni(o,qlist[i]);
                i++;
              
        })
    })
    }
    popuni(host,el)
    {
       
       console.log(el);
        let n=host.querySelector(".nazivploce");
        n.value=el.ime;
        console.log(n);
        n=host.querySelector(".izvodjacploce");
        n.value=el.izvodjac.ime;

       

        n=host.querySelector(".godstampanjaploce");
        n.value=el.godinastampanja;

        n=host.querySelector(".pesmeploce");
        n.value=el.pesme;
        n=host.querySelector(".cenaploce");
        n.value=el.cena;

    }
    crtajDiv(kontForma){
        let d=document.createElement("div");
        kontForma.appendChild(d);
        return d;

        }

    crtajLabelu(kontForma,unutrasnjiHTML){
        let lab=document.createElement("label");
        lab.className="lab";
        lab.innerHTML=unutrasnjiHTML;
        kontForma.appendChild(lab);


    }

    crtajSelect(sel){
        let p=this.kont.querySelector(".PRODAVNICA");
        p=p.innerHTML;
        fetch("https://localhost:5001/Prodavnica/PrikaziZanrove/"+ p,
        {
            method:"GET",

        }
        ).then(p=>{
            if(p.ok)
            {
                this.ocisti(sel);
                    p.json().then(zanrovi=>{
                        console.log(zanrovi);
                        zanrovi.forEach(i=>{
                            let op=document.createElement("option");
                            op.value=i.naziv;
                            console.log(i);
                            op.innerHTML=this.IzBrojaUZanr(i.naziv);
                            sel.appendChild(op);
                        });
                    })
                }
            }
        )
    }
    ocisti(l)
    {
        while (l.firstChild) {
         
            l.lastChild.innerHTML=" ";
            l.removeChild(l.lastChild);
            
          }
          console.log(l);
    }
    crtajPloce(host){
            this.kont=document.createElement("div");
            this.kont.className="GomilaPloca";
            host.appendChild(this.kont);

            var button = document.createElement("button");
            button.innerHTML = "Do Something";

            host = document.getElementsByTagName("body")[0];
            host.appendChild(button);

            // 3. Add event handler
            button.addEventListener ("click", function() {
            alert("did something");
            });
            const BtnAdd=document.querySelector(".btn.add");
            BtnAdd.addEventListener("click", dodajPlocu);
    
            function dodajPlocu(){
                const newDiv=document.createElement("div");
                console.log("add");
    
            }
    
    }
    

    IzZanraUBroj(zanr) 
    {
        switch(zanr)
      
        {
            case "Klasično": return 0;
            break;
            case "Pop": return 1;
            break;
            case "Trip_hop": return 2;
            break;
            case "Hip_hop": return 3;
            break;
            case "Džez": return 4;
            break;
            case "Bluz": return 5;
            break;
            case "Metal": return 6;
            break;
            case "Rok": return 7;
            break;
            case "Fank": return 8;
            break;
            case "Indi": return 9;
            break;
            case "Alternativno": return 10;
            break;
            case "Funk": return 11;
            break;
            case "Grandž": return 12;
            break;
            case "RnB": return 13;
            break;

        }
    }

    IzBrojaUZanr(zanr2) 
    {
        switch(zanr2)
      
        {
            case 0 : return "Klasično";
            break;
            case 1: return "Pop";
            break;
            case 2: return "Trip_hop";
            break;
            case 3: return "Hip_hop";
            break;
            case 4: return "Džez";
            break;
            case 5: return "Bluz";
            break;
            case 6: return "Metal";
            break;
            case 7: return "Rok";
            break;
            case 8: return "Fank";
            break;
            case 9: return "Indi";
            break;
            case 10: return "Alternativno";
            break;
            case 11: return "Funk";
            break;
            case 12: return "Grandž";
            break;
            case 13: return "RnB";
            break;

        }
    }

    


}     