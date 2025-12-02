export interface VaardigheidInstanceDto {
  Naam?: string;
  Niveau?: number;
}

export interface CvDto {
  Id: string;
  Auteur?: string;
  Email?: string;
  Adres?: string;
  Inleiding?: string;
  WerkervaringInstances?: string[];
  OpleidingInstances?: string[];
  CertificaatInstances?: string[];
  VaardigheidInstances?: VaardigheidInstanceDto[];
  IsFavorite?: boolean;
  LastUpdated?: string;
}
