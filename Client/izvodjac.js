export class Izvodjac{
    
    constructor(id,ime,){
        this.id=id;
        this.ime=ime;

    }
    dodajIzvodjaca(){
        if(p==null) {alert("Molimo unesite ime izvodjaca!"); return;}
        fetch("https://localhost:5001/Izvodjac/DodajIzvodjacaAkoNePostoji/" + p,
        {
            method: "POST",

        }).then(p => {
           
            if (p.ok) 
            alert("Uspešno ste dodali izvodjaca!");   
            
        });
    }       
}