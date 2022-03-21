export class Prodavac{

    constructor(ime, prezime, brojtelefona, licnakarta){
        this.ime = ime;
        this.prezime = prezime;
        this.brojtelefona = brojtelefona;
        this.licnakarta = licnakarta;
        this.miniCont=null;
        
    }

    crtajTabelu(host){
        let tabela=document.createElement("table");
        tabela.className="tabela";
        host.appendChild(tabela);

        let tabelahead=document.createElement("thead");
        tabelahead=["Ime", "Prezime", "Broj telefona", "Lična karta", "Obriši"];
        console.log(tabelahead);
        tabela.appendChild(tabelahead);

        this.crtajProdavca(tabela);



    }

    crtajProdavca(host){


        var tr=document.createElement("tr");
        host.appendChild(tr);


        var u=document.createElement("td");
        u.innerHTML=this.ime;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.prezime;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.brojtelefona;
        tr.appendChild(u);

        var u=document.createElement("td");
        u.innerHTML=this.licnakarta;
        tr.appendChild(u);
        

    }

}

