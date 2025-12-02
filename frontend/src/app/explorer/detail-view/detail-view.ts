import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import {
  CVDetails,
  DateParts,
  CVExperienceItem,
  CVEducationItem,
  SkillItem,
  Certification,
  LanguageItem,
} from '../../models/cv.models';
import { MatTableModule } from '@angular/material/table';

@Component({
  standalone: true,
  selector: 'explorer-detail-view',
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
  @Input() details: any | null = null;
  @Input() isFavoriteById: ((id?: string) => boolean) | undefined;

  @Output() toggleFavorite = new EventEmitter<string>();

  toggle(item: CVExperienceItem) {
    this.expandedItem = this.isExpanded(item) ? null : item;
  }

  get dataSource(): CVExperienceItem[] {
    return this.getExperience(this.details);
  }

  columnsToDisplay = ['Rol', 'Bedrijf', 'Periode'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  expandedItem: CVExperienceItem | null = null;
  isExpanded(item: CVExperienceItem): boolean {
    return this.expandedItem === item;
  }

  getPropertyByColumn(item: CVExperienceItem, column: string): any {
    switch (column) {
      case 'Rol':
        return item.position;
      case 'Bedrijf':
        return item.company;
      case 'Periode':
        return this.formatExperiencePeriod(item);
      default:
        return (item as any)[column];
    }
  }

  onToggleFavorite(cvId?: string, event?: MouseEvent) {
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    if (!cvId) return;
    this.toggleFavorite.emit(cvId);
  }

  getPrimaryTitle(details: CVDetails | null): string | null {
    if (!details || !details.experience || details.experience.length === 0) return null;
    const e = details.experience[0];
    const position = e.position ?? '';
    const company = e.company ? ` • ${e.company}` : '';
    return `${position}${company}`.trim();
  }

  private formatPeriod(start?: DateParts | null, end?: DateParts | null): string {
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
    if (typeof skill === 'object') {
      const label = String(skill.name ?? 'noname').trim();
      if (label.length > 0) return label;
      return '';
    }
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
