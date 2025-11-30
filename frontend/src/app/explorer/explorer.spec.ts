import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideZonelessChangeDetection } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { Explorer } from './explorer';

describe('Explorer', () => {
  let component: Explorer;
  let fixture: ComponentFixture<Explorer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Explorer],
      providers: [provideZonelessChangeDetection(), provideHttpClient()],
    }).compileComponents();

    fixture = TestBed.createComponent(Explorer);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
