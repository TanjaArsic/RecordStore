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
    crtajDiv1(PlocaTabela){
        let d=document.createElement("div");
        PlocaTabela.appendChild(d);
        return d;

    }

   crtajVinyl(host)
    {
        let miniCont=document.createElement("div");
        miniCont.className="jedanvinyl";
        host.appendChild(miniCont);

        let obrisibtn=document.createElement("button");
        obrisibtn.classList="obrisibtn";
        obrisibtn.innerHTML=this.cena + "din.";
        obrisibtn.onclick=ev=>{
            
            this.deleteVinyl(this.id)
            let parent=miniCont.parentNode;
            parent.removeChild(miniCont);
            console.log(this.id);
            

        }
        miniCont.appendChild(obrisibtn);

        // let izmenibtn=document.createElement("button");
        // izmenibtn.classList="izmenibtn";
        // izmenibtn.innerHTML="this.cena + "din;
        // izmenibtn.onclick=ev=>{
            
        //     if(this.deleteVinyl(this.id)){
        //     let parent=miniCont.parentNode;
        //     parent.removeChild(miniCont);
        //     console.log(this.id);
        //     }

        // }
        // miniCont.appendChild(izmenibtn);

        let d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.ime);

       d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.izvodjac.ime);

       d=this.crtajDiv1(miniCont);
       this.crtajLabelu1(d,this.pesme);
       

    //    d=this.crtajDiv1(miniCont);
    //    this.crtajLabelu1(d,this.cena);


    }

    vratiPesme(){
        return this.pesme;
    }

    deleteVinyl(id){
        let pare;
        pare = prompt("Unesite novac:", " ")

        if(pare < this.cena) {
            alert("Nema dovoljno novca"); 
            return;
        }

        fetch("https://localhost:5001/Vinyl/ObrisiPlocu/" + id, {
            method: "DELETE",
            }).then(p=>{
                if(p.ok){
                    let kusur = pare-this.cena;
                    alert("Uspešno kupljena ploča! Kusur je " + kusur + " din.");
                    
                }
            })

    }
        
    
    
}