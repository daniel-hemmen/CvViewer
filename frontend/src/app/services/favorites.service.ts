import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class FavoritesService {
  private favorites = new Set<string>();
  private favoritesSubject = new BehaviorSubject<Set<string>>(new Set());

  favorites$ = this.favoritesSubject.asObservable();

  private storageKey = 'cvFavorites';

  constructor() {
    try {
      const raw = localStorage.getItem(this.storageKey);
      const arr = raw ? JSON.parse(raw) : [];
      if (Array.isArray(arr)) {
        this.favorites = new Set(arr.filter((v) => typeof v === 'string'));
      }
    } catch (e) {
      this.favorites = new Set();
    }
    this.emit();
  }

  initialize(ids: string[] = []) {
    this.favorites = new Set(ids || []);
    this.save();
    this.emit();
  }

  toggle(id: string): boolean {
    if (!id) return false;
    if (this.favorites.has(id)) {
      this.favorites.delete(id);
    } else {
      this.favorites.add(id);
    }
    this.save();
    this.emit();
    return this.favorites.has(id);
  }

  isFavorite(id: string): boolean {
    return this.favorites.has(id);
  }

  getCount(): number {
    return this.favorites.size;
  }

  private emit() {
    this.favoritesSubject.next(new Set(this.favorites));
  }

  private save() {
    try {
      localStorage.setItem(this.storageKey, JSON.stringify(Array.from(this.favorites)));
    } catch (e) {}
  }
}
