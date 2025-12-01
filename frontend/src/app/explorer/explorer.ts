import { Component, signal, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MatTreeModule, MatTreeNestedDataSource } from '@angular/material/tree';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { DetailView } from './detail-view/detail-view';
import {
  CVNode,
  CVDetails,
  DateParts,
  CVExperienceItem,
  CVEducationItem,
  SkillItem,
  Certification,
  LanguageItem,
} from '../models/cv.models';
import { FavoritesService } from '../services/favorites.service';
import { environment } from '../../environments/environment.development';

interface CvDto {
  id: string;
  auteur?: string;
  email?: string;
  adres?: string;
  inleiding?: string;
  werkervaringInstances?: string[];
  opleidingInstances?: string[];
  certificaatInstances?: string[];
  vaardigheidInstances?: { beschrijving: string; niveau: number }[];
  isFavorite?: boolean;
  lastUpdated?: string;
}

@Component({
  selector: 'app-explorer',
  imports: [
    CommonModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatDividerModule,
    MatChipsModule,
    MatExpansionModule,
    DetailView,
  ],
  templateUrl: './explorer.html',
  styleUrl: './explorer.scss',
})
export class Explorer implements OnInit {
  private http = inject(HttpClient);
  private cd = inject(ChangeDetectorRef);
  private favService = inject(FavoritesService);

  private SimpleTreeControl = class<T> {
    dataNodes: T[] = [];
    private expanded = new Set<T>();
    constructor(private childrenAccessor: (node: T) => T[] | undefined | null) {}
    isExpanded(node: T) {
      return this.expanded.has(node);
    }
    toggle(node: T) {
      if (this.isExpanded(node)) {
        this.collapse(node);
      } else {
        this.expand(node);
      }
    }
    expand(node: T) {
      this.expanded.add(node);
    }
    collapse(node: T) {
      this.expanded.delete(node);
    }
    expandAll() {
      const visit = (nodes: T[] | undefined | null) => {
        if (!nodes) return;
        for (const n of nodes) {
          this.expanded.add(n);
          const children = this.childrenAccessor(n);
          if (children && children.length) {
            visit(children);
          }
        }
      };
      visit(this.dataNodes);
    }
    collapseAll() {
      this.expanded.clear();
    }
  };

  treeControl = new this.SimpleTreeControl<CVNode>((node) => node.children);
  dataSource = new MatTreeNestedDataSource<CVNode>();
  childrenAccessor = (node: CVNode) => node.children ?? [];

  protected readonly selectedCV = signal<CVNode | null>(null);
  protected readonly cvDetails = signal<CVDetails | null>(null);

  private cvDetailsMap: Map<string, CVDetails> = new Map();

  ngOnInit() {
    this.loadCVDetails();
  }

  private loadCVDetails() {
    this.http.get<CvDto[]>(`${environment.apiUrl}/api/cvs/all`).subscribe({
      next: (cvDtos) => {
        const details = cvDtos.map((dto) => this.mapDtoToDetails(dto));

        details.forEach((cv) => this.cvDetailsMap.set(cv.id, cv));

        const allChildren: CVNode[] = details.map((cv) => ({
          name: cv.name,
          type: 'file',
          id: cv.id,
        }));

        const allRoot: CVNode = {
          name: 'All',
          type: 'folder',
          children: allChildren,
        };

        const buildAndSetData = () => {
          const favChildren = allChildren.filter((c) => !!c.id && this.favService.isFavorite(c.id));
          const favRoot: CVNode = {
            name: 'Favorites',
            type: 'folder',
            children: favChildren,
          };
          const data: CVNode[] = [favRoot, allRoot];
          this.dataSource.data = data;
          this.treeControl.dataNodes = data;
        };

        buildAndSetData();

        this.favService.favorites$.subscribe(() => {
          buildAndSetData();
          this.cd.detectChanges();
        });
        setTimeout(() => {
          this.treeControl.expandAll();
          this.cd.detectChanges();
        }, 0);
      },
      error: (error) => {
        console.error('Error loading CV details:', error);
      },
    });
  }

  private mapDtoToDetails(dto: CvDto): CVDetails {
    return {
      id: dto.id,
      name: dto.auteur || 'Unknown',
      email: dto.email,
      location: dto.adres,
      summary: dto.inleiding,
      experience:
        dto.werkervaringInstances?.map((exp) => ({
          company: exp,
          position: '',
          startDate: null,
          endDate: null,
        })) || [],
      education:
        dto.opleidingInstances?.map((edu) => ({
          institution: edu,
          degree: '',
          startDate: null,
          endDate: null,
        })) || [],
      certifications:
        dto.certificaatInstances?.map((cert) => ({
          name: cert,
        })) || [],
      skills:
        dto.vaardigheidInstances?.map((skill) => ({
          name: skill.beschrijving,
          level: skill.niveau,
        })) || [],
      languages: [],
      lastUpdated: dto.lastUpdated,
    };
  }

  isFavoriteById(id?: string): boolean {
    if (!id) return false;
    return this.favService.isFavorite(id);
  }

  toggleFavoriteFromDetails(cvId?: string, event?: MouseEvent) {
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    if (!cvId) return;
    const newVal = this.favService.toggle(cvId);
    const det = this.cvDetailsMap.get(cvId);
    if (det) {
      (det as any).__favorite = newVal;
      this.cvDetailsMap.set(cvId, det);
    }
    if (this.cvDetails() && this.cvDetails()!.id === cvId) {
      this.cvDetails.set(this.cvDetailsMap.get(cvId) ?? null);
    }
    this.cd.detectChanges();
  }

  hasChild = (_: number, node: CVNode) => {
    return node.type === 'folder';
  };

  toggleNode(node: CVNode, event?: MouseEvent) {
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    this.treeControl.toggle(node as any);
  }

  toggleFavoriteNode(node: CVNode, event?: MouseEvent) {
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    if (!node || !node.id) return;
    const id = node.id;
    const newVal = this.favService.toggle(id);
    const det = this.cvDetailsMap.get(id);
    if (det) {
      (det as any).__favorite = newVal;
      this.cvDetailsMap.set(id, det);
      if (this.cvDetails() && this.cvDetails()!.id === id) {
        this.cvDetails.set(det);
      }
    }
    this.cd.detectChanges();
  }

  onNodeClick(node: CVNode, event?: MouseEvent) {
    if (event) {
      const target = event.target as HTMLElement | null;
      if (target && target.closest('button')) {
        return;
      }

      event.preventDefault();
      event.stopPropagation();
    }

    if (node.type === 'file' && node.id) {
      const cvId = node.id;
      this.selectedCV.set(node);
      const details = this.cvDetailsMap.get(cvId);
      this.cvDetails.set(details || null);
    }
  }

  getNodeIcon(node: CVNode): string {
    if (node.type === 'folder') {
      return this.treeControl.isExpanded(node) ? 'folder_open' : 'folder';
    }
    return 'description';
  }

  getCVDetails() {
    return this.cvDetails();
  }

  getPrimaryTitle(details: CVDetails | null): string | null {
    if (!details || !details.experience || details.experience.length === 0) return null;
    const e = details.experience[0];
    const position = e.position ?? '';
    const company = e.company ? ` • ${e.company}` : '';
    return `${position}${company}`.trim();
  }

  formatPeriod(start?: DateParts | null, end?: DateParts | null): string {
    const fmt = (d?: DateParts | null) => {
      if (!d) return '';
      if (d.year && d.month) return `${String(d.year)}-${String(d.month).padStart(2, '0')}`;
      if (d.year) return String(d.year);
      return '';
    };
    const s = fmt(start);
    const e = fmt(end);
    if (!s && !e) return '';
    if (!e) return `${s} — Heden`;
    if (!s) return e;
    return `${s} — ${e}`;
  }

  formatEducationYear(edu: CVEducationItem | undefined): string {
    if (!edu) return '';
    if (edu.endDate && edu.endDate.year) return String(edu.endDate.year);
    if (edu.startDate && edu.startDate.year) return String(edu.startDate.year);
    return '';
  }

  formatCertificationDate(cert: Certification | undefined): string {
    if (!cert || !cert.date) return '';
    const d = cert.date;
    if (d.year && d.month && d.day)
      return `${d.year}-${String(d.month).padStart(2, '0')}-${String(d.day).padStart(2, '0')}`;
    if (d.year && d.month) return `${d.year}-${String(d.month).padStart(2, '0')}`;
    if (d.year) return String(d.year);
    return '';
  }

  getExperience(details: CVDetails | null): CVExperienceItem[] {
    return details?.experience ?? [];
  }

  getSkills(details: CVDetails | null): SkillItem[] {
    return details?.skills ?? [];
  }

  getEducation(details: CVDetails | null): CVEducationItem[] {
    return details?.education ?? [];
  }

  getCertifications(details: CVDetails | null): Certification[] {
    return details?.certifications ?? [];
  }

  getLanguages(details: CVDetails | null): LanguageItem[] {
    const raw: any = (details as any)?.languages ?? [];
    if (!raw || raw.length === 0) return [];
    if (typeof raw[0] === 'string') {
      return raw.map((s: string) => ({ name: s }));
    }
    return raw as LanguageItem[];
  }

  formatLanguage(lang: any): string {
    if (!lang) return '';
    if (typeof lang === 'string') return lang;
    const prof = lang.proficiency ? ` • ${lang.proficiency}` : '';
    return `${lang.name}${prof}`;
  }

  formatExperiencePeriod(exp: any): string {
    if (!exp) return '';
    const p = this.formatPeriod(exp.startDate, exp.endDate);
    if (p) return p;
    return exp.period ?? '';
  }

  formatSkillLabel(skill: any): string {
    if (!skill) return '';
    if (typeof skill === 'object') return skill.name ?? JSON.stringify(skill);
    return String(skill);
  }

  formatSkillLevel(skill: any): string {
    if (!skill) return '';
    if (typeof skill === 'object' && skill.level) return `(${skill.level})`;
    return '';
  }

  skillDotStates(level?: number): boolean[] {
    const l = Math.max(0, Math.min(5, Number(level) || 0));
    return Array.from({ length: 5 }, (_, i) => i < l);
  }
}
