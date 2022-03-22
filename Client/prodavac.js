export class Prodavac{

    constructor(ime, prezime, brojtelefona, licnakarta){
        this.ime = ime;
        this.prezime = prezime;
        this.brojtelefona = brojtelefona;
        this.licnakarta = licnakarta;
        this.miniCont=null;
        
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

