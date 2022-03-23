import { Prodavnica } from "./prodavnica.js";
export class Vinyl {

    constructor(id, ime, zanr, pesme, godinaStampanja, cena, kolicina, izvodjac) {
        this.id = id;
        this.ime = ime;
        this.zanr = zanr;
        this.pesme = pesme;
        this.godinastampanja = godinaStampanja;
        this.cena = cena;
        this.kolicina = kolicina;
        this.izvodjac = izvodjac

    }
    

    crtajLabelu1(kontForma, unutrasnjiHTML) {
        let lab = document.createElement("label");
        lab.className = "lab1";
        lab.innerHTML = unutrasnjiHTML;
        kontForma.appendChild(lab);


    }
    crtajDiv1(miniCont) {
        let d = document.createElement("div");
        miniCont.appendChild(d);
        return d;

    }

    crtajVinyl(host, forma) {
        let miniCont = document.createElement("div");
        miniCont.className = "jedanvinyl";
        host.appendChild(miniCont);

        let obrisibtn = document.createElement("button");
        obrisibtn.classList = "obrisibtn";
        obrisibtn.innerHTML = this.cena + "din.";
        obrisibtn.onclick = ev => {
            
            this.deleteVinyl(miniCont)
            // let parent = miniCont.parentNode;
            // parent.removeChild(miniCont);

        }
        miniCont.appendChild(obrisibtn);

        let d = this.crtajDiv1(miniCont);
        this.crtajLabelu1(d, this.ime + " [" + this.godinastampanja + "]");

        d = this.crtajDiv1(miniCont);
        this.crtajLabelu1(d, "-" + this.izvodjac.ime);

        // d = this.crtajDiv1(miniCont);  radi zanrrrr
        // this.crtajLabelu1(d, this.zanr);

        console.log(this.zanr);



        var d1 = document.createElement("div");
        this.crtajLabelu1(d1, this.pesme);
        d1.className = "pesmeeee";
        miniCont.appendChild(d1);

        const popuni = ()=> {
            let e = new Prodavnica();
            e.popuni(forma, this);
        }

        miniCont.addEventListener("click", popuni);

    }
    vratiID() {
        return id;
    }

    deleteVinyl(miniCont) {
        let pare;
        pare = prompt("Unesite novac:", " ")

        if (pare < this.cena) {
            alert("Nema dovoljno novca");
            return;
        }

        if(this.kolicina>1){
            this.kolicina=this.kolicina-1;
        if(this.ime==null) {alert("ime je null"); return; }
        if(this.izvodjac==null) {alert("izvodjac je null"); return; }
            fetch("https://localhost:5001/Vinyl/SmanjiKolicinuPloce/" + this.ime + "/" + this.izvodjac,
            {
                method: "PUT",
        
            }).then(p => {
                
                if (p.ok) {
                alert("Uspesno promenjena kolicina!");  
                }
                else alert("Greska u promeni kolicine");
            }) 
            alert("Uspešno kupljena ploča! Preostala količina je " + this.kolicina);
        }

        else if(this.kolicina<1){
            alert ("WTF");
            return;
        }
        else{
        
        fetch("https://localhost:5001/Vinyl/ObrisiPlocu/" + this.id, {
            method: "DELETE",
        }).then(p => {
            if (p.ok) {
                let kusur = pare - this.cena;
                alert("Uspešno kupljena ploča! Kusur je " + kusur + " din.");
                // let miniCont = this.kont.querySelector(".jedanvinyl");
                // let parent = miniCont.parentNode;
                // parent.removeChild(miniCont);
                miniCont.remove();
            }
        })

    }
}


}