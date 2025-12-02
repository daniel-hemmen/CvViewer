import { Component, inject, signal, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

import { MatTreeModule, MatTreeNestedDataSource } from '@angular/material/tree';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';

import { DetailView } from './detail-view/detail-view';
import { FavoritesService } from '../services/favorites.service';
import { environment } from '../../environments/environment';

/* -----------------------------------------------------------
   Interfaces
----------------------------------------------------------- */

export interface CVNode {
  name: string;
  type: 'folder' | 'file';
  id?: string;
  children?: CVNode[];
}

export interface DateParts {
  year?: number;
  month?: number;
  day?: number;
}

export interface CVExperienceItem {
  company: string;
  position: string;
  startDate?: DateParts | null;
  endDate?: DateParts | null;
  description?: string;
  location?: string;
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
  date?: DateParts | null;
  expiration?: DateParts | null;
  url?: string;
}

export interface SkillItem {
  name: string;
  level?: number;
}

export interface CVDetails {
  id: string;
  name: string;
  email?: string;
  phone?: string;
  location?: string;
  summary?: string;
  experience: CVExperienceItem[];
  education: CVEducationItem[];
  certifications: Certification[];
  skills: SkillItem[];
  lastUpdated?: string;
  __favorite?: boolean;
}

/* -----------------------------------------------------------
   Component
----------------------------------------------------------- */

@Component({
  selector: 'app-explorer',
  standalone: true,
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
  private favorites = inject(FavoritesService);

  dataSource: CVNode[] = [];
  treeControl = new NestedTreeControl<CVNode>((node) => node.children);
  selectedCV = signal<CVNode | null>(null);
  cvDetails = signal<CVDetails | null>(null);
  cvDetailsMap = new Map<string, CVDetails>();

  readonly childrenAccessor = (node: CVNode) => node.children ?? [];

  /* -----------------------------------------------------------
     Lifecycle
  ----------------------------------------------------------- */
  ngOnInit(): void {
    this.loadCVList();
  }

  /* -----------------------------------------------------------
     Load Data
  ----------------------------------------------------------- */

  private loadCVList(): void {
    this.http.get<any[]>(`${environment.apiUrl}/api/cvs/all`).subscribe({
      next: (dtos) => {
        const favoritedIds = dtos.filter((d) => d.isFavorited).map((d) => d.id);
        this.favorites.initialize(favoritedIds);

        const details = dtos.map((dto) => this.mapDto(dto));
        details.forEach((d) => this.cvDetailsMap.set(d.id, d));

        const allChildren = details.map((d) => ({
          name: d.name,
          type: 'file' as const,
          id: d.id,
        }));

        const favoriteChildren = allChildren.filter((n) => n.id && this.favorites.isFavorite(n.id));

        this.dataSource = [
          { name: 'Favorites', type: 'folder', children: favoriteChildren },
          { name: 'All', type: 'folder', children: allChildren },
        ];

        this.cd.detectChanges();
      },
    });
  }

  /* -----------------------------------------------------------
     Node Interaction
  ----------------------------------------------------------- */

  toggleNode(node: CVNode, event?: MouseEvent): void {
    if (event) {
      event.stopPropagation();
    }
    if (this.treeControl.isExpanded(node)) {
      this.treeControl.collapse(node);
    } else {
      this.treeControl.expand(node);
    }
  }

  isFavoriteById(id?: string): boolean {
    return id ? this.favorites.isFavorite(id) : false;
  }

  getCVDetails(): CVDetails | null {
    return this.cvDetails();
  }

  onNodeClick(node: CVNode, event?: MouseEvent): void {
    if (event) {
      const element = event.target as HTMLElement;
      if (element.closest('button')) return;
      event.preventDefault();
      event.stopPropagation();
    }

    if (node.type === 'file' && node.id) {
      this.selectedCV.set(node);
      this.cvDetails.set(this.cvDetailsMap.get(node.id) ?? null);
    }
  }

  toggleFavoriteNode(node: CVNode, event?: MouseEvent): void {
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    if (!node.id) return;

    this.updateFavorite(node.id);
  }

  toggleFavoriteFromDetails(cvId: string | undefined, event?: MouseEvent): void {
    if (!cvId) return;

    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }

    this.updateFavorite(cvId);
  }

  private updateFavorite(id: string): void {
    this.favorites.toggle(id).subscribe((newValue) => {
      const details = this.cvDetailsMap.get(id);

      if (details) {
        details.__favorite = newValue;
        this.cvDetailsMap.set(id, details);

        if (this.cvDetails()?.id === id) {
          this.cvDetails.set(details);
        }
      }

      this.refreshTree();
      this.cd.detectChanges();
    });
  }

  private refreshTree(): void {
    const allDetails = Array.from(this.cvDetailsMap.values());
    const allChildren = allDetails.map((d) => ({
      name: d.name,
      type: 'file' as const,
      id: d.id,
    }));

    const favoriteChildren = allChildren.filter((n) => n.id && this.favorites.isFavorite(n.id));

    const favoritesExpanded = this.dataSource[0]
      ? this.treeControl.isExpanded(this.dataSource[0])
      : false;
    const allExpanded = this.dataSource[1]
      ? this.treeControl.isExpanded(this.dataSource[1])
      : false;

    const favoritesNode: CVNode = { name: 'Favorites', type: 'folder', children: favoriteChildren };
    const allNode: CVNode = { name: 'All', type: 'folder', children: allChildren };

    this.dataSource = [favoritesNode, allNode];

    if (favoritesExpanded) this.treeControl.expand(favoritesNode);
    if (allExpanded) this.treeControl.expand(allNode);
  }

  /* -----------------------------------------------------------
     Helpers
  ----------------------------------------------------------- */

  hasChild = (_: number, node: CVNode) => node.type === 'folder';

  getNodeIcon(node: CVNode): string {
    return node.type === 'folder' ? 'folder' : 'description';
  }

  /* -----------------------------------------------------------
     Data Mapping
  ----------------------------------------------------------- */

  private mapDto(dto: any): CVDetails {
    return {
      id: dto.id,
      name: dto.auteur,
      email: dto.email,
      phone: dto.telefoonnummer,
      location: dto.adres,
      summary: dto.inleiding,
      experience: (dto.werkervaringInstances ?? []).map((x: any) => ({
        company: x.organisatie,
        position: x.rol,
        startDate: parseDate(x.startdatum),
        endDate: parseDate(x.einddatum),
        description: x.beschrijving,
        location: x.plaats,
      })),
      education: (dto.opleidingInstances ?? []).map((x: any) => ({
        institution: x.instituut,
        degree: x.naam,
        startDate: parseDate(x.startdatum),
        endDate: parseDate(x.einddatum),
        description: x.beschrijving,
      })),
      certifications: (dto.certificaatInstances ?? []).map((x: any) => ({
        name: x.naam,
        issuer: x.uitgever,
        date: parseDate(x.datumAfgifte),
        expiration: parseDate(x.verloopdatum),
        url: x.url,
      })),
      skills: (dto.vaardigheidInstances ?? []).map((x: any) => ({
        name: x.naam,
        level: x.niveau,
      })),
      lastUpdated: dto.lastUpdated,
      __favorite: dto.isFavorited,
    };
  }

  getSkills(d: CVDetails | null): SkillItem[] {
    return d?.skills ?? [];
  }

  getEducation(d: CVDetails | null): CVEducationItem[] {
    return d?.education ?? [];
  }

  getCertifications(d: CVDetails | null): Certification[] {
    return d?.certifications ?? [];
  }
}

/* -----------------------------------------------------------
   Utility
----------------------------------------------------------- */

function parseDate(value: string | null | undefined): DateParts | null {
  if (!value) return null;

  // yyyy-mm-dd
  const iso = value.match(/^(\d{4})-(\d{2})-(\d{2})$/);
  if (iso) return { year: +iso[1], month: +iso[2], day: +iso[3] };

  // dd-mm-yyyy
  const dmy = value.match(/^(\d{2})-(\d{2})-(\d{4})$/);
  if (dmy) return { year: +dmy[3], month: +dmy[2], day: +dmy[1] };

  // mm-yyyy
  const my = value.match(/^(\d{2})-(\d{4})$/);
  if (my) return { year: +my[2], month: +my[1] };

  // yyyy
  const y = value.match(/^(\d{4})$/);
  if (y) return { year: +y[1] };

  return null;
}
