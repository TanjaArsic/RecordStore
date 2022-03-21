import { Vinyl } from "./vinyl.js";
import{ Prodavac } from "./prodavac.js";

export class Prodavnica{
    constructor(id,naziv,adresa,mail,brVinyl){
        this.id=id;
        this.naziv=naziv;
        this.adresa=adresa;
        this.mail=mail;
        this.brVinyl=brVinyl;
        this.kont=null;    
        
    }

    dodajiliIzmeniPlocu(pl){
        let p=this.kont.querySelector(".PRODAVNICA");
        p=p.innerHTML;

        let q=this.kont.querySelector(".nazivploce");
        q=q.value;

        let q1=this.kont.querySelector(".izvodjacploce");
        q1=q1.value;

        // let q2=this.kont.querySelector(".izdavackakucaploce");
        // q2=q2.innerHTML;
        let q3=this.kont.querySelector(".godstampanjaploce");
        q3=q3.value;
        let q4=this.kont.querySelector(".pesmeploce");
        q4=q4.value;
        let q5=this.kont.querySelector(".cenaploce");
        q5=q5.value;
        
        let m=this.kont.querySelector(".selektZanra");
        let zanr=this.kont.querySelector("select").value;

        console.log(m);
        console.log(zanr);

        // if(btn=="OK"){
            fetch("https://localhost:5001/SpojProdavnicaPloca/DodajPlocu/" + p + "/" + q + "/" + q1 + "/" + q3 + "/" + zanr +"/" + q4 +"/" + q5,
            {
                method: "POST",
        
            }).then(p => {
                if(q3<1930 || q3>2022) alert("Molimo unesite validnu godinu štampanja ploče!");
                if(q5<0) alert("Unesite validnu cenu ploče!");
                if (p.ok) 
                alert("Uspešno ste dodali ploču!");   
                else alert("Neuspešno dodavanje ploče!")
                
            });
        

        // else if(btn=="Izmeni"){

        // }
    }

    crtaj(kont, gost){  //<!---crtajprodavnicu--->

        kont.replaceChildren();

        this.kont=kont;

        let prodavnica=document.createElement("div");
        prodavnica.className="hue";
        this.kont.appendChild(prodavnica);

        let PRODAVNICA =document.createElement("label");
        PRODAVNICA.className="PRODAVNICA";
        PRODAVNICA.innerHTML=gost;
        prodavnica.appendChild(PRODAVNICA);

        let adresa=document.createElement("div");
        adresa.className="adresa";
        prodavnica.appendChild(adresa);
///////////////////////////////////////////////////////////kako se ovo radi
        let p=this.kont.querySelector(".PRODAVNICA");
        p=p.innerHTML;
        console.log(p);
        let q=document.createElement("label");
        q.innerHTML="Adresa: "+ p.adresa;///////////////////////////////////////zasto je undefined
        console.log(q);

        q.className="podacioprodavnici";
        adresa.appendChild(q);

        let madresa=document.createElement("div");
        madresa.className="adresa";
        prodavnica.appendChild(madresa);
        let q1=document.createElement("label");
        q1.innerHTML=" Mail: "+ this.mail;
        q1.className="podacioprodavnici";
        madresa.appendChild(q1);

///////////////////

        let navbar=document.createElement("navbar");
        navbar.className="navbar";
        this.kont.appendChild(navbar);

        let btn1=document.createElement("button");
        btn1.innerHTML="PLOČE";
        btn1.className="btns";
        navbar.appendChild(btn1);
        btn1.onclick=(ev)=>this.prikaziploce();

        let btn2=document.createElement("button");
        btn2.innerHTML="PRODAVCI";
        btn2.className="btns";
        navbar.appendChild(btn2);
        btn2.onclick=(ev)=>this.prikaziprodavce();

        let VelikiDiv=document.createElement("div");
        VelikiDiv.className="VELIKIDIV";
        this.kont.appendChild(VelikiDiv);

        let kontForma=document.createElement("div");
        kontForma.className="Forma";
        VelikiDiv.appendChild(kontForma);

        let PlocaTabela=document.createElement("div");
        PlocaTabela.className="PlocaTabela";
        VelikiDiv.appendChild(PlocaTabela);

        let deoZaRadnike=document.createElement("div");
        deoZaRadnike.className="RADNICI";
        this.kont.appendChild(deoZaRadnike);

        let tabelaRadnici=document.createElement("div");
        tabelaRadnici.className="tabelaRadnici";
        deoZaRadnike.appendChild(tabelaRadnici);


        
        // const btn=document.createElement("button");
        // btn.addEventListener=(ev)

    

         this.crtajFormu(kontForma);
        //  this.crtajProdavca(host);
        
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
        this.crtajLabelu(nekidiv,"Godina štampanja: ");
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
        this.crtajLabelu(nekidiv,"Žanr: ");
        let sel=document.createElement("select");
        sel.className="selektZanra";
        sel.name="selektZanra";
        nekidiv.appendChild(sel);

        nekidiv=this.crtajDiv(kontForma);
        this.crtajLabelu(nekidiv,"Cena: ");
        input=document.createElement("input");
        input.className="cenaploce";
        nekidiv.appendChild(input);

        nekidiv=this.crtajDiv(kontForma);
        let btn=document.createElement("button");
        btn.innerHTML="OK";
        btn.className="btn";
        nekidiv.appendChild(btn);
        btn.onclick=(ev)=>this.dodajiliIzmeniPlocu();

        // nekidiv=this.crtajDiv(kontForma);
        // let btn1=document.createElement("button");
        // btn1.innerHTML="Prikazi ploče";
        // btn1.className="btn";
        // nekidiv.appendChild(btn1);
        // btn1.onclick=(ev)=>this.prikaziploce();

        this.crtajSelect(sel);
    }

   


    prikaziploce()
    {
        let m=this.kont.querySelector(".PlocaTabela");
        this.ocisti(m);
        // document.querySelector(".PlocaTabela").innerHTML = "";
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
            })//////////////////////////////////////
            let e=new Prodavnica();
            let o=this.kont.querySelector(".kontForma");
            console.log(qlist);
            let n=document.querySelectorAll(".PlocaTabela");
            n.forEach(function(a){
            a.addEventListener("click", function(){
                
            e.popuni(o,qlist[i]); //qlist je niz ploca
            i++;
              
        })
    })
    }


    popuni(host,el){
       
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

        // n=this.kont.querySelector(".btn");
        // n.innerHTML="Izmeni";
        // n.value=el.id;
        

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
                        // console.log(zanrovi);
                        zanrovi.forEach(i=>{
                            let op=document.createElement("option");
                            op.value=i.naziv;
                            // console.log(i);
                            op.innerHTML=this.IzBrojaUZanr(i.naziv);
                            sel.appendChild(op);
                        });
                    })
                }
            }
        )
    }

    ocisti(l) {
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
            BtnAdd.addEventListener("click", dodajiliIzmeniPlocu);
    
            function dodajiliIzmeniPlocu(){
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

    // crtajProdavca(host){


    //     let kontPrikaz=document.createElement("div");
    //     kontPrikaz.className="sveZaradnike";
    //     host.appendChild(kontPrikaz);

    //     let tabela=document.createElement("table");
    //     tabela.className="tabela";
    //     kontPrikaz.appendChild(tabela);

    //     let tabelahead=document.createElement( "thead");
    //     tabela.appendChild(tabelahead);

    //     let tr=document.createElement("tr");
    //     tabelahead.appendChild(tr);

    //     let tabelaBody=document.createElement("tbody");
    //     tabelaBody.className="TabelaPodaci";
    //     tabela.appendChild(tabelaBody);

    //     let th;
    //     var zag=["Ime", "Prezime", "Broj telefona", "Lična karta"];
    //     console.log(zag);
    //     zag.forEach(el=>{
    //         th=document.createElement("th");
    //         th.innerHTML=el; //stringovi sa zaglavlj
    //         tr.appendChild(th);
    //     })

    //     // let btn2=document.createElement("button");
    //     // btn2.innerHTML="Prikazi prodavce";
    //     // btn2.className="btn";
    //     // kontPrikaz.appendChild(btn2);
    //     // btn2.onclick=(ev)=>this.prikaziprodavce();

    // }

    prikaziprodavce()
    {
        let a=this.kont.querySelector(".tabelaRadnici");
        let p=this.kont.querySelector(".PRODAVNICA");
        p=p.innerHTML;
        let q;
        fetch("https://localhost:5001/Prodavac/PrikaziProdavca/"+ p,
        {
            method:"GET",

        }).then(p=>{
            if(p.ok)
            {                
            p.json().then(prodavci=>{

                if(prodavci.length===0)
                    alert("Nema radnika u prodavnici");

                else{
                    // let body=this.OcistiTablicu();
                    prodavci.forEach(p=>{
                        // console.log(p);

                    q=new Prodavac(p.ime, p.prezime, p.brojTelefona, p.licnaKarta);
                    console.log(q);
                    q.crtajProdavca(a);
                })
                // this.OtvoriTabelu();
                // console.log(qlist);
            }
            
         })

        }
        // qlist.forEach(k=>{
        //     k.crtaj(m);
        //     console.log(1);
        // })
            
    })
    }
    NacrtajTablicu()
    {
        
        let divTabela=this.kontejner.querySelector(".tabelaRadnici");
        console.log(divTabela);
        let tabela=document.createElement("table");
        tabela.className="Tablica";
        divTabela.appendChild(tabela);

        var head=document.createElement("thead");
        tabela.appendChild(head);

        var tr=document.createElement("tr");
        head.appendChild(tr);

        var body=document.createElement("tbody");
        body.className="Telo";
        tabela.appendChild(body);

        let td;
        var niz=["Ime","Prezime","Broj telefona","Licna karta"];
        niz.forEach(i=>{
            td=document.createElement("td");
            td.innerHTML=i;
            head.appendChild(td);
            console.log(td);
        });

        
      }
    
    // OtvoriTabelu()
    // {
        
    //     let divTabela=this.kontejner.querySelector(".tabelaRadnici");
    //     if( divTabela.style.display=='block')
    //        divTabela.style.display='none';
    //     else
    //     divTabela.style.display='block';
        
     
        
    // }
    // OcistiTablicu(host)
    // {
    //     let divTabela=this.kont.querySelector(".TABELA");
    //     let t=divTabela.querySelector(".Telo");
    //     while(t.firstChild)
    //         t.removeChild(t.lastChild);
    //     return t;
        
    // }
    


}     