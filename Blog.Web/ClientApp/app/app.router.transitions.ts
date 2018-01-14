import { trigger, animate, style, group, animateChild, query, stagger, transition } from '@angular/animations';

export const routerTransition = trigger('routerTransition', [
  transition('* <=> *', [
    query(':enter', style({ position: 'fixed', width: '100%' }), { optional: true }),
    query(':leave', style({ position: 'fixed', width: '100%' }), { optional: true }),
    group([
      query(':enter', [
        style({ transform: 'translate(10vh,20vw)', opacity: 0 }),
        animate('0.5s ease-in-out', style({ transform: 'translate(0,0)', opacity: 1 }))
      ], { optional: true }),
      query(':leave', [
        style({ transform: 'translate(0,0)' , opacity: 1 }),
        animate('0.5s ease-in-out', style({ transform: 'translate(10vh,20vw)', opacity: 0 }))
      ], { optional: true }),
    ])
  ])
])
