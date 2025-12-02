import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

// Angular Material
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatTableModule } from '@angular/material/table';

// -----------------------
// Interfaces
// -----------------------

interface DateParts {
  year?: number;
  month?: number;
  day?: number;
}

interface CVExperienceItem {
  company: string;
  position: string;
  startDate?: DateParts | null;
  endDate?: DateParts | null;
  description?: string;
  location?: string;
}

interface CVEducationItem {
  institution: string;
  degree: string;
  startDate?: DateParts | null;
  endDate?: DateParts | null;
  description?: string;
}

interface Certification {
  name: string;
  issuer?: string;
  date?: DateParts | null;
  expiration?: DateParts | null;
  url?: string;
}

interface SkillItem {
  name: string;
  level?: number;
}

interface CVDetails {
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
}

// -----------------------
// Component
// -----------------------

@Component({
  selector: 'explorer-detail-view',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatCardModule,
    MatChipsModule,
  ],
  templateUrl: './detail-view.html',
  styleUrl: './detail-view.scss',
})
export class DetailView {
  // -----------------------
  // Inputs & Outputs
  // -----------------------
  @Input() details: CVDetails | null = null;
  @Input() isFavoriteById?: (id?: string) => boolean;
  @Output() toggleFavorite = new EventEmitter<string>();

  // Track which experience item is expanded
  expandedItem: CVExperienceItem | null = null;

  // -----------------------
  // Expansion Logic
  // -----------------------
  toggle(item: CVExperienceItem): void {
    this.expandedItem = this.isExpanded(item) ? null : item;
  }

  isExpanded(item: CVExperienceItem): boolean {
    return this.expandedItem === item;
  }

  // -----------------------
  // Table Configuration
  // -----------------------
  get dataSource(): CVExperienceItem[] {
    return this.details?.experience ?? [];
  }

  readonly columnsToDisplay = ['Rol', 'Bedrijf', 'Periode'];
  readonly columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];

  getPropertyByColumn(item: CVExperienceItem, column: string): string {
    switch (column) {
      case 'Rol':
        return item.position;
      case 'Bedrijf':
        return item.company;
      case 'Periode':
        return this.formatExperiencePeriod(item);
      default:
        return (item as any)[column] ?? '';
    }
  }

  // -----------------------
  // Favorites
  // -----------------------
  onToggleFavorite(cvId?: string, event?: MouseEvent): void {
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    if (cvId) {
      this.toggleFavorite.emit(cvId);
    }
  }

  // -----------------------
  // Titles & Formatting
  // -----------------------
  getPrimaryTitle(details: CVDetails | null): string | null {
    if (!details?.experience?.length) return null;
    const first = details.experience[0];
    const pos = first.position ?? '';
    const company = first.company ? ` • ${first.company}` : '';
    return `${pos}${company}`.trim();
  }

  // -----------------------
  // Helpers: Formatting
  // -----------------------
  private formatDateParts(d?: DateParts | null): string {
    if (!d) return '';
    if (d.year && d.month && d.day)
      return `${d.year}-${String(d.month).padStart(2, '0')}-${String(d.day).padStart(2, '0')}`;
    if (d.year && d.month) return `${d.year}-${String(d.month).padStart(2, '0')}`;
    if (d.year) return `${d.year}`;
    return '';
  }

  formatPeriod(start?: DateParts | null, end?: DateParts | null): string {
    const s = this.formatDateParts(start);
    const e = this.formatDateParts(end);

    if (!s && !e) return '';
    if (!e) return `${s} — Heden`;
    if (!s) return e;

    return `${s} — ${e}`;
  }

  formatExperiencePeriod(item: CVExperienceItem): string {
    return this.formatPeriod(item.startDate, item.endDate);
  }

  getSkills(d: CVDetails | null): SkillItem[] {
    return d?.skills ?? [];
  }

  formatSkillLabel(skill: SkillItem): string {
    return skill.name;
  }

  skillDotStates(level?: number): boolean[] {
    const max = 5;
    const current = level ?? 0;
    return Array.from({ length: max }, (_, i) => i < current);
  }

  getEducation(d: CVDetails | null): CVEducationItem[] {
    return d?.education ?? [];
  }

  formatEducationYear(edu: CVEducationItem): string {
    return this.formatPeriod(edu.startDate, edu.endDate);
  }

  getCertifications(d: CVDetails | null): Certification[] {
    return d?.certifications ?? [];
  }

  formatCertificationDate(cert: Certification): string {
    return this.formatDateParts(cert.date);
  }
}
