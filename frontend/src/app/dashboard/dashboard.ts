import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { HttpClient } from '@angular/common/http';

interface DashboardSummary {
  totalCVs: number;
  recentlyUpdated: number;
  favorites: number;
  views: number;
}

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {
  private http = inject(HttpClient);

  totalCVs: number | null = null;
  recentlyUpdated: number | null = null;
  favorites: number | null = null;
  views: number | null = null;

  // ngOnInit(): void {
  //   this.loadSummary();
  // }
  // loadSummary(): void {
  //   this.http.get<DashboardSummary>('/api/dashboard/summary').subscribe({
  //     next: (s) => {
  //       this.totalCVs = s.totalCVs ?? 0;
  //       this.recentlyUpdated = s.recentlyUpdated ?? 0;
  //       this.favorites = s.favorites ?? 0;
  //       this.views = s.views ?? 0;
  //     },
  //     error: (err) => {
  //       console.error('Failed to load dashboard summary', err);
  //       this.totalCVs = this.totalCVs ?? 0;
  //       this.recentlyUpdated = this.recentlyUpdated ?? 0;
  //       this.favorites = this.favorites ?? 0;
  //       this.views = this.views ?? 0;
  //     },
  //   });
  // }
}
