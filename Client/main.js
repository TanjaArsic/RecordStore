import { Prodavnica } from "./prodavnica.js";

var listaProdavnica = [];
fetch("https://localhost:5001/Prodavnica/Prodavnica")
.then(p=>{
    p.json().then(prodavnice=>{ //jsonu imamo listu prodavnica
        let pocetni=document.createElement("div");
        pocetni.className="PocetniDIV";
        document.body.appendChild(pocetni);
        
        let labela=document.createElement("label");
        labela.innerHTML="Prodavnica";
        labela.className="Labela";
        pocetni.appendChild(labela);

        let sel=document.createElement("select");
        sel.className="Select";
        sel.innerHTML="prodavnica";
        pocetni.appendChild(sel);

        let kont=document.createElement("div");
        kont.className="GlavniKontejner";
        document.body.appendChild(kont);



        let op;
        let sava;
        prodavnice.forEach(store=>{ //tu listu prodavnica obidjemo //i za svaku prodavnicu da obidje i da kaze sve ove stvari
            var p = new Prodavnica(store.id, store.naziv, store.adresa, store.mail, store.brVinyla);
            sava=p;
            op=document.createElement("option");
            op.innerHTML=p.naziv;
            op.value=p.id;
            op.className="Pocetna";
            sel.appendChild(op);
            listaProdavnica.push(p);
            op.addEventListener("click", function(){
                // gKont.forEach(function(k){
                //     alert("123");
                //     k.removeChild(gKont.children[0]);
                // });
                sava.crtaj(kont,p.naziv);
                
                
            })
        })
        let i=0;
       // console.log(listaProdavnica[0].naziv);
        let kakoosh=document.querySelectorAll(".Pocetna");
        // const gKont=document.querySelectorAll(".GlavniKontejner");
        kakoosh.forEach(function(a){
            
        })    
    })

})

console.log(listaProdavnica);

