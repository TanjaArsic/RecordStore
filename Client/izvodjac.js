export class Izvodjac{
    
    constructor(id,ime,){
        this.id=id;
        this.ime=ime;

    }
    dodajIzvodjaca(){
        fetch("https://localhost:5001/Izvodjac/DodajIzvodjacaAkoNePostoji/" + p,
        {
            method: "POST",

        }).then(p => {
            if(p==null) alert("Molimo unesite ime izvodjaca!");
            else (p.ok) 
            alert("Uspešno ste dodali izvodjaca!");   
            
        });
    }       
}