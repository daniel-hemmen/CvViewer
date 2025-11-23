export interface CVNode {
  name: string;
  type: 'folder' | 'file';
  id?: string;
  children?: CVNode[];
}

export interface DateParts {
  year?: number | null;
  month?: number | null;
  day?: number | null;
}

export interface CVExperienceItem {
  company: string;
  department?: string | null;
  position: string;
  startDate?: DateParts | null;
  endDate?: DateParts | null;
  description?: string;
}

export interface CVEducationItem {
  institution: string;
  degree: string;
  startDate?: DateParts | null;
  endDate?: DateParts | null;
  description?: string;
}

export interface Certification {
  name: string;
  issuer?: string;
  date?: DateParts;
}

export interface SkillItem {
  name: string;
  level?: number;
}

export interface LanguageItem {
  name: string;
  proficiency?: string;
}

export interface CVDetails {
  id: string;
  name: string;
  email?: string;
  phone?: string;
  location?: string;
  summary?: string;
  experience?: CVExperienceItem[];
  education?: CVEducationItem[];
  certifications?: Certification[];
  skills?: SkillItem[];
  languages?: LanguageItem[];
  lastUpdated?: string;
}
