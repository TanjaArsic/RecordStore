export class Vinyl{

    constructor(id, ime, zanr, pesme, godinaStampanja,cena,kolicina,izvodjac){
        this.id = id;
        this.ime = ime;
        this.zanr = zanr;
        this.pesme = pesme;
        this.godinastampanja = godinaStampanja;
        this.cena=cena;
        this.kolicina=kolicina;
        this.izvodjac=izvodjac
       
        
        
    }

    crtajLabelu1(kontForma,unutrasnjiHTML){
        let lab=document.createElement("label");
        lab.className="lab1";
        lab.innerHTML=unutrasnjiHTML;
        kontForma.appendChild(lab);


    }
    crtajDiv1(kontForma){
        let d=document.createElement("div");
        kontForma.appendChild(d);
        return d;

    }

   crtajVinyl(host)
    {
        let miniCont=document.createElement("div");
        miniCont.className="jedanvinyl";
        host.appendChild(miniCont);

        let d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.ime);
       d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.izvodjac.ime);
       d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.pesme);

       d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.cena);




        


    }
    
}