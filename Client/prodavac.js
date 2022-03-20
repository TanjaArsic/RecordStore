export class Prodavac{

    constructor(id, ime, prezime, brojtelefona, licnakarta){
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.brojtelefona = brojtelefona;
        this.licnakarta = licnakarta;
        this.miniCont=null;
        
    }

    crtajTabelu(m){
        
       
        let miniCont=document.createElement("div");
        miniCont.className="DIV-tabela";
        m.appendChild(miniCont);

        let tabela=document.createElement("table");
        tabela.className="tabela";
        miniCont.appendChild(tabela);

        let tabelahead=document.createElement("thead");
        tabela.appendChild(tabelahead);

        let tr=document.createElement("tr");
        tabelahead.appendChild(tr);

        let tabelaBody=document.createElement("tbody");
        tabelaBody.className="TabelaPodaci";
        tabela.appendChild(tabelaBody);

        let th;
        var zag=["Ime", "Prezime", "Broj telefona", "Lična karta"];
        zag.forEach(el=>{
            th=document.createElement("th");
            th.innerHTML=el;
            tr.appendChild(th);
        })

    }

}