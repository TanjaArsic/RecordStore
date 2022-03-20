export class Prodavac{

    constructor(id, ime, prezime, brojtelefona, licnakarta){
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.brojtelefona = brojtelefona;
        this.licnakarta = licnakarta;
        this.miniCont=null;
        
    }

    crtajProdavca(host){


        let kontPrikaz=document.createElement("div");
        kontPrikaz.className="sveZaradnike";
        host.appendChild(kontPrikaz);

        let tabela=document.createElement("table");
        tabela.className="tabela";
        kontPrikaz.appendChild(tabela);

        let tabelahead=document.createElement( "thead");
        tabela.appendChild(tabelahead);

        let tr=document.createElement("tr");
        tabelahead.appendChild(tr);

        let tabelaBody=document.createElement("tbody");
        tabelaBody.className="TabelaPodaci";
        tabela.appendChild(tabelaBody);

        let th;
        var zag=["Ime", "Prezime", "Broj telefona", "Lična karta"];
        console.log(zag);
        zag.forEach(el=>{
            th=document.createElement("th");
            th.innerHTML=el; //stringovi sa zaglavlj
            tr.appendChild(th);
        })

        // let btn2=document.createElement("button");
        // btn2.innerHTML="Prikazi prodavce";
        // btn2.className="btn";
        // kontPrikaz.appendChild(btn2);
        // btn2.onclick=(ev)=>this.prikaziprodavce();

    }

}

