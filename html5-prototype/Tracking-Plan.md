# Hong Kong Zombie Survival - Tracking Plan

## AppsFlyer Web SDK Integration

```html
<!-- Add to index.html head -->
<script>
  // AppsFlyer Web SDK
  (function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
  new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
  j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
  'https://websdk.appsflyer.com/afw.js?id='+i+dl;f.parentNode.insertBefore(j,f);
  })(window,document,'script','AF','YOUR_APP_ID');
</script>
```

## Core Events to Track

| Event | Trigger | Parameters |
|-------|---------|------------|
| `app_open` | Page load / game start | {game_version: "1.0.0"} |
| `level_start` | Start game | {level: 1, day: 1} |
| `level_complete` | Reach next day | {level: 1, day: N, score: X} |
| `level_fail` | Player dies | {level: 1, time_survived: X, score: Y} |
| `tutorial_complete` | First zombie kill | {time_to_complete: X} |
| `score_submit` | Score milestone | {score: X, zombies_killed: Y} |
| `session_end` | Tab close / refresh | {session_time: X, max_zombies: Y} |

## Implementation Code

```javascript
// Tracking wrapper
const Track = {
    send(event, params = {}) {
        if (typeof AF !== 'undefined') {
            AF('event', event, { ...params, ts: Date.now() });
        }
        console.log('[Track]', event, params);
        // Fallback: localStorage for testing
        localStorage.setItem('track_' + event, JSON.stringify(params));
    },
    
    appOpen() { this.send('app_open', { version: '1.0.0' }); },
    gameStart() { this.send('level_start', { level: 1, day: 1 }); },
    gameOver(score, time) { 
        this.send('level_fail', { 
            level: 1, 
            time_survived: time, 
            score: score 
        }); 
    },
    zombieKill() { this.send('score_submit', { event: 'kill' }); },
    dayComplete(day, score) {
        this.send('level_complete', { level: 1, day, score });
    }
};
```

## Key Metrics for CPI Test

| Metric | Target |
|--------|--------|
| CTR | > 2% |
| CVR | > 30% |
| D1 Retention | > 35% |
| Session Length | > 3 min |
| CPI | < $1.50 (Hybrid-Casual) |

## A/B Test Scenarios

1. **Hook A**: "Survive the Hong Kong Zombie Apocalypse!"
2. **Hook B**: "Build your base, fight endless zombies!"
3. **Hook C**: "Low-poly zombie survival with HK vibe"